
using System.Drawing.Imaging;
using Decimatio.Common.DTOs;

namespace Decimatio.Common.Services
{
    public class PDFGeneratorService : IPDFGeneratorService
    {
        private readonly string currentDirectory = Directory.GetCurrentDirectory() + "\\Template";
        private const float BackgroundImageOpacity = 0.15f;

        private static byte[]? _cachedFadedBackground;
        private static readonly object _fadeLock = new();

        public PDFGeneratorService()
        {

        }

        public byte[] GeneratePDFVoucher(string base64Pdf, RequestTicketBodyQRDto ticket)
        {
            try
            {
                string backgroundImage = Path.Combine(currentDirectory, "pruebamasacre.png");
                byte[] fadedBackground = GetFadedBackgroundImage(backgroundImage, BackgroundImageOpacity);

                var document = Document.Create(container =>
                {
                    container.Page(p =>
                    {
                        p.Size(PageSizes.A4);
                        p.Background().Image(fadedBackground).FitArea();
                        p.Header().Element(headerContainer => ComposeHeader(headerContainer, ticket));
                        p.Content().Element(contentContainer => ComposeContent(contentContainer, ticket, base64Pdf));
                    });
                });

                using var memoryStream = new MemoryStream();
                document.GeneratePdf(memoryStream);
                byte[] documentPdfBytes = memoryStream.ToArray();
                return documentPdfBytes;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo generar el pdf", ex);
            }
        }

        public string CombinePdfFiles(List<string> strList)
        {
            try
            {
                using (PdfDocument outputPDFDocument = new PdfDocument())
                {
                    foreach (string pdfFile in strList)
                    {
                        byte[] pdfBytes = Convert.FromBase64String(pdfFile);
                        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
                        {
                            using (PdfDocument inputPDFDocument = PdfReader.Open(pdfStream, PdfDocumentOpenMode.Import))
                            {
                                foreach (PdfPage page in inputPDFDocument.Pages)
                                {
                                    outputPDFDocument.AddPage(page);
                                }
                            }
                        }
                    }

                    using (MemoryStream stream = new MemoryStream())
                    {
                        outputPDFDocument.Save(stream, false);
                        stream.Position = 0;

                        byte[] pdfBytes = stream.ToArray();
                        string base64String = Convert.ToBase64String(pdfBytes);
                        return base64String;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al combinar los PDF's: {ex.Message}", ex);
            }
        }

        private Stream Base64ToImageStream(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes);
            return ms;
        }

        private byte[] GetFadedBackgroundImage(string imagePath, float opacity)
        {
            if (_cachedFadedBackground is not null)
                return _cachedFadedBackground;

            lock (_fadeLock)
            {
                if (_cachedFadedBackground is not null)
                    return _cachedFadedBackground;

                using var original = new Bitmap(imagePath);

                // A4 aspect ratio (width/height). Crop the source image to this ratio so it
                // covers the full page with no letterboxing once placed as the page background.
                const float pageAspect = 595.28f / 841.89f;
                float sourceAspect = (float)original.Width / original.Height;

                Rectangle cropSource;
                if (sourceAspect > pageAspect)
                {
                    int croppedWidth = (int)(original.Height * pageAspect);
                    int x = (original.Width - croppedWidth) / 2;
                    cropSource = new Rectangle(x, 0, croppedWidth, original.Height);
                }
                else
                {
                    int croppedHeight = (int)(original.Width / pageAspect);
                    int y = (original.Height - croppedHeight) / 2;
                    cropSource = new Rectangle(0, y, original.Width, croppedHeight);
                }

                using var faded = new Bitmap(cropSource.Width, cropSource.Height);
                using var graphics = System.Drawing.Graphics.FromImage(faded);

                var colorMatrix = new ColorMatrix { Matrix33 = opacity };
                using var imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                graphics.DrawImage(original, new Rectangle(0, 0, cropSource.Width, cropSource.Height), cropSource.X, cropSource.Y, cropSource.Width, cropSource.Height, GraphicsUnit.Pixel, imageAttributes);

                using var ms = new MemoryStream();
                faded.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                _cachedFadedBackground = ms.ToArray();
                return _cachedFadedBackground;
            }
        }

        private void ComposeHeader(IContainer container, RequestTicketBodyQRDto ticket)
        {
            //string logoImage = Path.Combine(currentDirectory, "logoMors2.png");

            container.Background(Colors.Black).Row(row =>
            {
                row.RelativeItem()
                    .PaddingLeft(10)
                    .PaddingVertical(10)
                    .Column(col =>
                {
                    col.Item().Text($"{ticket.ProductoraResponsable} Presenta").FontSize(13).FontColor(Colors.White).SemiBold();
                });

                row.RelativeItem()
                    .PaddingVertical(10)
                    .PaddingRight(25)
                    .Column(col =>
                {
                    col.Item().Text($"Ticket N°{ticket.IdTicket}").FontSize(11).FontColor(Colors.White);
                });

                //row.ConstantItem(45).Padding(5).Column(col =>
                //{
                //    col.Item().Image(logoImage);
                //});
            });
        }

        private void ComposeContent(IContainer container, RequestTicketBodyQRDto ticket, string base64Pdf)
        {
            string warningIconImage = Path.Combine(currentDirectory, "attention.png");
            string formatDay = ticket.FechaEvento.ToString("dddd", new CultureInfo("es-ES"));
            string anio = ticket.FechaEvento.ToString("yyyy", new CultureInfo("es-ES"));
            string formatDate = ticket.FechaEvento.ToString("d' de 'MMMM", new CultureInfo("es-ES"));
            string formatHora = ticket.FechaEvento.ToString("HH:mm");
            long montoTotalFormat = (long)ticket.MontoTotal;
            string pais = "Chile";

            container.Column(col =>
            {
                col.Item().PaddingTop(20).Row(row =>
                    {
                        row.RelativeItem()
                            .Padding(25)
                            .Column(innerCol =>
                            {
                                innerCol.Item().Text($"{ticket?.NombreEvento}").FontSize(15).SemiBold().FontColor(Colors.Black);
                                innerCol.Item().Text($"Fecha: {formatDay.ToUpper()}, {formatDate} {anio}").FontSize(12).FontColor(Colors.Black);
                                innerCol.Item().Text($"Hora: {formatHora}").FontSize(12).FontColor(Colors.Black);

                                innerCol.Spacing(5);

                                innerCol.Item().Text($"{ticket?.NombreLugar} #{ticket?.Numeracion}").FontSize(14).SemiBold().FontColor(Colors.Black);
                                innerCol.Item().Text($"{ticket?.NombreComuna}, {pais}").FontSize(12).FontColor(Colors.Black);

                                innerCol.Spacing(5);

                                innerCol.Item().Text($"Sector: {ticket?.NombreSector}").FontSize(14).SemiBold().FontColor(Colors.Black);
                                innerCol.Item().Text($"Valor: ${montoTotalFormat}").FontColor(Colors.Black);

                            });

                        row.RelativeItem().Column(innerCol =>
                        {
                            innerCol.Item().Height(300).AlignCenter().AlignMiddle().Element(innerContainer =>
                            {
                                var imageStream = Base64ToImageStream(base64Pdf);
                                innerContainer.Image(imageStream).FitArea();
                            });

                        });
                    });

                col.Item().Row(row =>
                {
                    row.RelativeItem()
                        .Height(100)
                        .Padding(25)
                        .Text(text =>
                        {
                            text.Span("Este es un ticket electrónico. Por favor, muestra este ticket para ingresar al evento.").FontColor(Colors.Black);
                        });
                });

                col.Spacing(5);
                col.Item().Row(row =>
                {
                    row.RelativeItem()
                        .Padding(10)
                        .Column(col =>
                        {
                            col.Item().Text("NO HAGAS COPIAS DE ESTA ENTRADA, SOLAMENTE EL PRIMERO EN PASAR POR LOS LECTORES TENDRÁ ACCESO AL EVENTO.").FontSize(10).SemiBold().FontColor(Colors.Black);
                            col.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                            col.Item().Text("Este ticket es tu entrada al evento, posee un código único e intransferible y es la garantía de acceso. Si tienes algún problema sobre el acceso del ticket, por favor comunicate con nosotros para ayudarte.")
                                .FontSize(9).FontColor(Colors.Black);
                            col.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                            col.Item().Text("Para ingresar al evento deberás presentar este ticket con el QR legible y el documento de identidad asociado a esta entrada").FontSize(9).FontColor(Colors.Black);
                            col.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                        });

                    row.ConstantItem(45).Padding(5).Column(col =>
                    {
                        col.Item().Image(warningIconImage);
                    });
                });
            });

        }
    }
}

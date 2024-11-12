namespace Decimatio.Common.Services
{
    internal sealed class PDFGeneratorService : IPDFGeneratorService
    {
        private readonly string currentDirectory = Directory.GetCurrentDirectory() + "\\Template";

        public PDFGeneratorService()
        {
        }

        public byte[] GeneratePDFVoucher(string base64Pdf, TicketBodyQRDto ticket)
        {
            try
            {
                var document = Document.Create(container => 
                {
                    container.Page(p => 
                    {
                        p.Size(PageSizes.A4);
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

        private void ComposeHeader(IContainer container, TicketBodyQRDto ticket)
        {
            string logoImage = Path.Combine(currentDirectory, "logoMors2.png");

            container.Background(Colors.Black).Row(row =>
            {
                row.RelativeItem()
                    .PaddingLeft(10)
                    .PaddingTop(10)
                    .Column(col =>
                {
                    col.Item().Text($"{ticket.Evento.ProductoraResponsable} Presenta").FontSize(13).FontColor(Colors.White).SemiBold();
                });

                row.RelativeItem()
                    .PaddingTop(10)
                    .PaddingRight(25)
                    .Column(col =>
                {
                    col.Item().Text($"Ticket N°{ticket.IdTicket}").FontSize(11).FontColor(Colors.White);
                });

                row.ConstantItem(45).Padding(5).Column(col =>
                {
                    col.Item().Image(logoImage);
                });
            });
        }

        private void ComposeContent(IContainer container, TicketBodyQRDto ticket, string base64Pdf)
        {
            string warningIconImage = Path.Combine(currentDirectory, "attention.png");
            string logoImage = Path.Combine(currentDirectory, "fondoMorsVicitOmnia.png");
            string formatDay = ticket.Evento.Fecha.ToString("dddd", new CultureInfo("es-ES"));
            string anio = ticket.Evento.Fecha.ToString("yyyy", new CultureInfo("es-ES"));
            string formatDate = ticket.Evento.Fecha.ToString("d' de 'MMMM", new CultureInfo("es-ES"));
            string formatHora = ticket.Evento.Fecha.ToString("HH:mm");
            long montoTotalFormat = (long)ticket.MontoTotal;
            string pais = "Chile";

            container.Column(col =>
            {
                col.Item().Element(innerContainer =>
                {
                    innerContainer.Image(logoImage);
                });

                col.Item().PaddingTop(-370).Row(row =>
                    {
                        row.RelativeItem()
                            .Padding(25)
                            .Column(innerCol =>
                            {
                                innerCol.Item().Text($"{ticket?.Evento?.NombreEvento}").FontSize(15).SemiBold().FontColor(Colors.Black).BackgroundColor(Colors.Grey.Darken1);
                                innerCol.Item().Text($"Fecha: {formatDay.ToUpper()}, {formatDate} {anio}").FontSize(12).FontColor(Colors.Black).BackgroundColor(Colors.Grey.Darken1);
                                innerCol.Item().Text($"Hora: {formatHora}").FontSize(12).FontColor(Colors.Black).BackgroundColor(Colors.Grey.Darken1);

                                innerCol.Spacing(5);

                                innerCol.Item().Text($"{ticket?.Evento?.Lugar?.NombreLugar} #{ticket?.Evento?.Lugar?.Numeracion}").FontSize(14).SemiBold().FontColor(Colors.Black).BackgroundColor(Colors.Grey.Darken1);
                                innerCol.Item().Text($"{ticket?.Evento?.Lugar?.Comuna?.NombreComuna}, {pais}").FontSize(12).FontColor(Colors.Black).BackgroundColor(Colors.Grey.Darken1);

                                innerCol.Spacing(5);

                                innerCol.Item().Text($"Sector: {ticket?.Sector?.NombreSector}").FontSize(14).SemiBold().FontColor(Colors.Black).BackgroundColor(Colors.Grey.Darken1);
                                innerCol.Item().Text($"Valor: ${montoTotalFormat}").FontColor(Colors.Black).BackgroundColor(Colors.Grey.Darken1);

                            });

                        row.RelativeItem().Column(innerCol =>
                        {
                            innerCol.Item().Height(300).Element(innerContainer =>
                            {
                                var imageStream = Base64ToImageStream(base64Pdf);
                                innerContainer.Image(imageStream);
                            });

                        });
                    });

                col.Item().Row(row =>
                {
                    row.RelativeItem()
                        .Height(100)
                        .Background(Colors.Black)
                        .Padding(25)
                        .Text(text =>
                        {
                            text.Span("Este es un ticket electrónico. Por favor, muestra este ticket para ingresar al evento.").FontColor(Colors.White);
                        });
                });

                col.Spacing(5);
                col.Item().Row(row =>
                {
                    row.RelativeItem()
                        .Background(Colors.Grey.Lighten3)
                        .Padding(10)
                        .Column(col =>
                        {
                            col.Item().Text("NO HAGAS COPIAS DE ESTA ENTRADA, SOLAMENTE EL PRIMERO EN PASAR POR LOS LECTORES TENDRÁ ACCESO AL EVENTO.").FontSize(10).SemiBold();
                            col.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                            col.Item().Text("Este ticket es tu entrada al evento, posee un código único e intransferible y es la garantía de acceso. Si tienes algún problema sobre el acceso del ticket, por favor comunicate con nosotros para ayudarte.")
                                .FontSize(9);
                            col.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                            col.Item().Text("Para ingresar al evento deberás presentar este ticket con el QR legible y el documento de identidad asociado a esta entrada").FontSize(9);
                            col.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                        });

                    row.ConstantItem(45).Background(Colors.Grey.Lighten3).Padding(5).Column(col =>
                    {
                        col.Item().Image(warningIconImage);
                    });
                });
            });

        }
    }
}

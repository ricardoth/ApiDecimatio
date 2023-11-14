using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Globalization;

namespace Decimatio.Common.Services
{
    public sealed class PDFGeneratorService : IPDFGeneratorService
    {
        private readonly EmailConfig _emailConfig;

        public PDFGeneratorService(EmailConfig emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public byte[] GeneratePDFVoucher(string base64Pdf, TicketBodyQRDto ticket)
        {
            string currentDirectory = Directory.GetCurrentDirectory() + "\\Template";
            string logoImage = Path.Combine(currentDirectory, "decimatio2.jpg");
            string warningIconImage = Path.Combine(currentDirectory, "attention.png");
            string formatDay = ticket.Evento.Fecha.ToString("dddd", new CultureInfo("es-ES"));
            string anio = ticket.Evento.Fecha.ToString("yyyy", new CultureInfo("es-ES"));
            string formatDate = ticket.Evento.Fecha.ToString("d' de 'MMMM", new CultureInfo("es-ES"));
            string formatHora = ticket.Evento.Fecha.ToString("HH:mm");
            long montoTotalFormat = (long)ticket.MontoTotal;
            string pais = "Chile";

            try
            {
                var document = Document.Create(container => 
                {
                    container.Page(p => 
                    {
                        p.Size(PageSizes.A4);

                        p.Header().Background(Colors.Black).Row(row => 
                        {
                            row.RelativeItem()
                                .PaddingLeft(10)
                                .PaddingTop(10)
                                .Column(col => 
                            {
                                col.Item().Text("Ticketera Presenta").FontSize(13).FontColor(Colors.White).SemiBold();
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

                        p.Content().Column(col => 
                        {
                            col.Item().Row(row => 
                            {
                                row.RelativeItem()
                                    .Padding(25)
                                    .Column(innerCol =>
                                    {
                                        innerCol.Item().Text($"{ticket?.Evento?.NombreEvento}").FontSize(15).SemiBold();
                                        innerCol.Item().Text($"Fecha: {formatDay.ToUpper()}, {formatDate} {anio}").FontSize(12);
                                        innerCol.Item().Text($"Hora: {formatHora}").FontSize(12);

                                        innerCol.Spacing(5);

                                        innerCol.Item().Text($"{ticket?.Evento?.Lugar?.NombreLugar} #{ticket?.Evento?.Lugar?.Numeracion}").FontSize(14).SemiBold();
                                        innerCol.Item().Text($"{ticket?.Evento?.Lugar?.Comuna?.NombreComuna}, {pais}").FontSize(12);

                                        innerCol.Spacing(5);

                                        innerCol.Item().Text($"Sector: {ticket?.Sector?.NombreSector}").FontSize(14).SemiBold();
                                        innerCol.Item().Text($"Valor: ${montoTotalFormat}");

                                    });

                                row.AutoItem().PaddingTop(10).PaddingBottom(10).LineVertical(1).LineColor(Colors.Grey.Medium);

                                row.ConstantItem(1).Background(Colors.White);
                               
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
                                        text.Span("Este es un ticket electrónico. Por favor, muestra este ticket para ingresar al evento. Para más información visita nuestra página web: ").FontColor(Colors.White);
                                        text.Hyperlink("facebook.com/kerygma.prods", "https://www.facebook.com/kerygma.prods").FontColor(Colors.White);
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
                                        col.Item().Text("Este ticket es tu entrada al evento, posee un código único e intransferible y es la garantía de acceso. Si tienes algún problema sobre el acceso del ticket, por favor cominucate con nosotros para ayudarte.")
                                            .FontSize(9);
                                        col.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Medium);
                                     
                                        col.Item().Text(text =>
                                        {
                                            text.Span("Ante cualquier duda o consulta acerca del evento o este ticket, puedes ponerte en contacto con nosotros enviando un correo a ").FontSize(9);
                                            text.Span($"{_emailConfig.From}.").FontSize(9).SemiBold();
                                        });
                                    });
                                   
                                row.ConstantItem(45).Background(Colors.Grey.Lighten3).Padding(5).Column(col =>
                                {
                                    col.Item().Image(warningIconImage);
                                });
                            });
                            
                        });

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
                using (PdfSharp.Pdf.PdfDocument outputPDFDocument = new PdfSharp.Pdf.PdfDocument())
                {
                    foreach (string pdfFile in strList)
                    {
                        byte[] pdfBytes = Convert.FromBase64String(pdfFile);
                        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
                        {
                            using (PdfSharp.Pdf.PdfDocument inputPDFDocument = PdfReader.Open(pdfStream, PdfDocumentOpenMode.Import))
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
    }
}

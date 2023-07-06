using System.Net.Mime;

namespace Decimatio.Common.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService()
        {
            _smtpClient = new SmtpClient
            {
                Host = "smpt-mail.outlook.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("tutorialseu-dev@outlook.com", "Test12345678!")
            };

        }

        public async Task SendEmail(string toAddress, string subject, string body, string pdfBase64)
        {
            var fromAddress = new MailAddress("tutorialseu-dev@outlook.com", "Quarzo");

            using (var message = new MailMessage(fromAddress, new MailAddress(toAddress)) 
            {
                Subject = subject,
                Body = body
            })
            {
                byte[] pdfBytes = Convert.FromBase64String(pdfBase64);
                MemoryStream ms = new MemoryStream(pdfBytes);
                Attachment attachment = new Attachment(ms, "Ticket.pdf", MediaTypeNames.Application.Pdf);
                message.Attachments.Add(attachment);

                try
                {
                    _smtpClient.Send(message);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al generar el Correo: {ex.Message}", ex);
                }
            }
        }
    }
}

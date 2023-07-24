namespace Decimatio.Common.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _emailConfig;

        public EmailService(EmailConfig emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendEmail(EmailTicketDto emailDto)
        {
            var email = new MimeMessage();
            email.Subject = emailDto.GetSubject();
            email.From.Add(new MailboxAddress("Remitente", _emailConfig.From));
            email.To.Add(new MailboxAddress("Destinatario", emailDto.GetAddress()));

            var bodyBuilder = new BodyBuilder { HtmlBody = emailDto.GetBody() };
            //Attach pdf
            byte[] pdfBytes = Convert.FromBase64String(emailDto.GetPdfBase64());
            MemoryStream ms = new MemoryStream(pdfBytes);

            var attachments = new MimePart("application", "pdf")
            {
                Content = new MimeContent(ms, ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = $"Ticket N° {emailDto.GetTicketBodyQRDto().IdTicket}"
            };

            bodyBuilder.Attachments.Add(attachments);
            email.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_emailConfig.Host, Convert.ToInt32(_emailConfig.Port),
                                MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailConfig.From, _emailConfig.Password);
                var response = await smtp.SendAsync(email);
                smtp.Disconnect(true);
            };
            ms.Close();
        }
    }
}

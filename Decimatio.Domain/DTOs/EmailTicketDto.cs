namespace Decimatio.Domain.DTOs
{
    public class EmailTicketDto
    {
        private string ToAddress { get; set; }
        private string Subject { get; set; }
        private string Body { get; set; }
        private string PdfBase64 { get; set; }
        private TicketBodyQRDto TicketBodyQRDto { get; set; }   

        public EmailTicketDto(string toAddress, string toSubject, string toBody, string toBase64Pdf, TicketBodyQRDto ticket)
        {
            ToAddress = toAddress;
            Subject = toSubject;
            Body = toBody;
            PdfBase64 = toBase64Pdf;
            TicketBodyQRDto = ticket;
        }

        public String GetAddress() => ToAddress;
        public String GetSubject() => Subject;
        public String GetBody() => Body;
        public String GetPdfBase64() => PdfBase64;
        public TicketBodyQRDto GetTicketBodyQRDto() => TicketBodyQRDto;
       
    }
}

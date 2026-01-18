namespace Decimatio.Application.DTOs
{
    public record EmailTicketDto
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Base64 { get; set; }
        public long IdTicket { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreEvento { get; set; }
        public string NombreLugar { get; set; }
        public string NombreSector { get; set; }
        public long MontoTotal { get; set; }
    }
}

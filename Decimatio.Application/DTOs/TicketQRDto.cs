namespace Decimatio.Application.DTOs
{
    public record TicketQRDto
    {
        public long IdTicketQR { get; set; }
        public long IdTicket { get; set; }
        public string Contenido { get; set; }
        public string NombreTicketComprobante { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        public TicketDto? Ticket { get; set; }
    }
}

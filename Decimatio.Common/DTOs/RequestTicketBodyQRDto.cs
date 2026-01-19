namespace Decimatio.Common.DTOs
{
    public record RequestTicketBodyQRDto
    {
        public long IdTicket { get; set; }
        public decimal MontoPago { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaTicket { get; set; }
        public string NombreEvento { get; set; }
        public string? ProductoraResponsable { get; set; }
        public DateTime FechaEvento { get; set; }
        public string? NombreLugar { get; set; }
        public string? Numeracion { get; set; }
        public string? NombreComuna { get; set; }
        public string? NombreSector { get; set; }
    }
}

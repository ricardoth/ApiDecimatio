namespace Decimatio.Application.DTOs
{
    public record TicketBodyQRDto
    {
        public long IdTicket { get; set; }
        public decimal MontoPago { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaTicket { get; set; }
        public UsuarioDto? Usuario { get; set; }
        public EventoDto? Evento { get; set; }
        public SectorDto? Sector { get; set; }
        public MedioPagoDto? MedioPago { get; set; }
    }
}

namespace Decimatio.Application.DTOs
{
    public class TicketDto
    {
        public long IdTicket { get; set; }
        public long IdUsuario { get; set; }
        public long IdEvento { get; set; }
        public long IdSector { get; set; }
        public long IdMedioPago { get; set; }
        public decimal MontoPago { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaTicket { get; set; }
        public bool Activo { get; set; }
        public UsuarioDto? Usuario { get; set; }
        public EventoDto? Evento { get; set; }
        public SectorDto? Sector { get; set; }
        public MedioPagoDto? MedioPago { get; set; }
    }
}

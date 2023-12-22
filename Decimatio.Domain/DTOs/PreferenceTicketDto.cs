namespace Decimatio.Domain.DTOs
{
    public class PreferenceTicketDto
    {
        public long IdPreference { get; set; }
        public string PreferenceCode { get; set; }
        public string TransactionId { get; set; }
        public int IdUsuario { get; set; }
        public int IdEvento { get; set; }
        public int IdSector { get; set; }
        public int IdMedioPago { get; set; }
        public decimal MontoPago { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaTicket { get; set; }
        public bool Activo { get; set; }
        public EventoDto? Evento { get; set; }
        public SectorDto? Sector { get; set; }
        public MedioPagoDto? MedioPago { get; set; }
    }
}

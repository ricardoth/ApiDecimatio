namespace Decimatio.Domain.Entities
{
    public class PreferenceTicket
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
        public bool Descargados { get; set; }
        public bool Activo { get; set; }
        public Usuario? Usuario { get; set; }
        public Evento? Evento { get; set; }
        public Sector? Sector { get; set; }
        public MedioPago? MedioPago { get; set;}
    }
}

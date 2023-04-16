namespace Decimatio.Domain.Entities
{
    public class TicketQR
    {
        public int IdTicketQR { get; set; }
        public long IdTicket { get; set; }
        public string Contenido { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

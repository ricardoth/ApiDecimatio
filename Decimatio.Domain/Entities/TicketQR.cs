namespace Decimatio.Domain.Entities
{
    public class TicketQR
    {
        public long IdTicketQR { get; set; }
        public long IdTicket { get; set; }
        public string Contenido { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Ticket Ticket { get; set; }
    }
}

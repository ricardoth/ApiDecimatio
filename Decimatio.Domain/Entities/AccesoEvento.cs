namespace Decimatio.Domain.Entities
{
    public class AccesoEvento
    {
        public long IdAccesoEvento { get; set; }
        public long IdTicket { get; set; }
        public DateTime? FechaHoraEntrada { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
        public Ticket Ticket { get; set; }
    }
}

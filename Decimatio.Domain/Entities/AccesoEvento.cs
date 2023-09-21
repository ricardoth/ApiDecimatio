namespace Decimatio.Domain.Entities
{
    public class AccesoEvento
    {
        public long IdAccesoEvento { get; set; }
        public long IdTicket { get; set; }
        public int IdEstadoTicket { get; set; }
        public DateTime? FechaHoraEntrada { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
    }
}

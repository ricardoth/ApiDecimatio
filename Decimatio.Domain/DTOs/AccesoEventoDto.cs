namespace Decimatio.Domain.DTOs
{
    public class AccesoEventoDto
    {
        public long IdAccesoEvento { get; set; }
        public long IdTicket { get; set; }
        public DateTime? FechaHoraEntrada { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
    }
}

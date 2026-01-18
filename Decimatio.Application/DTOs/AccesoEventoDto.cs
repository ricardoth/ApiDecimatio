namespace Decimatio.Application.DTOs
{
    public record AccesoEventoDto
    {
        public long IdAccesoEvento { get; set; }
        public long IdTicket { get; set; }
        public DateTime? FechaHoraEntrada { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
    }
}

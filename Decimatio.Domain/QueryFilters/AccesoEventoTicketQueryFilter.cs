namespace Decimatio.Domain.QueryFilters
{
    public class AccesoEventoTicketQueryFilter
    {
        public long IdAccesoEvento { get; set; }
        public long IdTicket { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstadoTicket { get; set; }
        public int IdEvento { get; set; }
        public int IdSector { get; set; }
        public DateTime? FechaHoraEntrada { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}

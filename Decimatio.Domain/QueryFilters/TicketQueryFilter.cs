namespace Decimatio.Domain.QueryFilters
{
    public class TicketQueryFilter
    {
        public long IdTicket { get; set; }
        public long IdUsuario { get; set; }
        public long IdEvento { get; set; }
        public long IdSector { get; set; }
        public long IdMedioPago { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}

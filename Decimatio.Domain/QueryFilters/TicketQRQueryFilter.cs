namespace Decimatio.Domain.QueryFilters
{
    public class TicketQRQueryFilter
    {
        public long IdTicketQR { get; set; }
        public long IdTicket { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}

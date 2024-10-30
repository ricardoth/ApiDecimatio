namespace Decimatio.Domain.Entities
{
    public class MerchantOrder
    {
        public long IdMerchantOrder { get; set; }
        public long? Id { get; set; }
        public string? Status { get; set; }
        public string? ExternalReference { get; set; }
        public string? PreferenceId { get; set; }
        public string? MarketPlace { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastUpdate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public bool Cancelled { get; set; }
        public string OrderStatus { get; set; }
    }
}

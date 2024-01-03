namespace Decimatio.Domain.IntegrationEntities
{
    public class PurchaseUnit
    {
        public List<Item> items { get; set; }
        public Amount amount { get; set; }
    }
}

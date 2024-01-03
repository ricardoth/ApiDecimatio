namespace Decimatio.Domain.IntegrationEntities
{
    public class Order
    {
        public string intent { get; set; }
        public List<PurchaseUnit> purchase_units { get; set; }
        public ApplicationContext application_context { get; set; }
    }
}

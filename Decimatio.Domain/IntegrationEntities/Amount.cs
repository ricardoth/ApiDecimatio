namespace Decimatio.Domain.IntegrationEntities
{
    public class Amount
    {
        public string currency_code { get; set; }
        public string value { get; set; }
        public Breakdown breakdown { get; set; }
    }
}

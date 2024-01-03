namespace Decimatio.Infraestructure.Models
{
    public class PaymentResponse
    {
        public string id { get; set; }
        public string status { get; set; }
        public IEnumerable<LinkResponse> links { get; set; }
    }
}

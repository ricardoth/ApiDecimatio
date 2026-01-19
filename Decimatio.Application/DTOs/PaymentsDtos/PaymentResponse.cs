namespace Decimatio.Application.DTOs.PaymentsDtos
{
    public class PaymentResponse
    {
        public string id { get; set; }
        public string status { get; set; }
        public IEnumerable<LinkResponse> links { get; set; }
    }
}

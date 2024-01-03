namespace Decimatio.Infraestructure.Contracts
{
    public interface IPayPalService
    {
        Task<string> CreateAccessToken();
        Task<PaymentResponse> CreatePayment(Order order);
        Task<string> CaptureOrder(string orderId);
    }
}

namespace Decimatio.Application.Interfaces.Services
{
    public interface IPayPalService
    {
        Task<string> CreateAccessToken();
        Task<PaymentResponse> CreatePayment(Order order);
        Task<string> CaptureOrder(string orderId);
    }
}

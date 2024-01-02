namespace Decimatio.Infraestructure.Contracts
{
    public interface IPayPalService
    {
        Task<string> CreateAccessToken();
        Task<string> CreatePayment();
    }
}

using Decimatio.Domain.MercadoPagoEntitites;

namespace Decimatio.Domain.Interfaces
{
    public interface IMercadoPagoRepository
    {
        Task<IEnumerable<PreferenceTicket>> GetAll();
        Task<int> AddNotificationPayment(MercadoPagoNotification notification);
    }
}

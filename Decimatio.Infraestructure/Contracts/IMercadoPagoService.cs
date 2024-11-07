using Decimatio.Domain.MercadoPagoEntitites;

namespace Decimatio.Infraestructure.Contracts
{
    public interface IMercadoPagoService
    {
        Task<IEnumerable<PreferenceTicket>> GetAllPreferenceTickets();
    }
}

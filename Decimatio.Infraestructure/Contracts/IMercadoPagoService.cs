using Decimatio.Domain.MercadoPagoEntitites;

namespace Decimatio.Infraestructure.Contracts
{
    public interface IMercadoPagoService
    {
        Task<Preference> CrearSolicitudPago(PreferenceData data);
        Task<IEnumerable<PreferenceTicket>> GetAllPreferenceTickets();
        Task<int> CrearNotificacionPago(MercadoPagoNotification notification);
    }
}

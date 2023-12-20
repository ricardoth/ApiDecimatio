using Decimatio.Domain.MercadoPagoEntities;
using MercadoPago.Resource.Customer;
using MercadoPago.Resource.Preference;

namespace Decimatio.Infraestructure.Contracts
{
    public interface IMercadoPagoService
    {
        Task<IList<Customer>> SearchCliente();
        Task<IList<CustomerCard>> GetTarjetasCliente();
        Task<bool> CrearClientePago();
        Task<Preference> CrearSolicitudPago(PreferenceData data);
    }
}

using Decimatio.Infraestructure.Models;
using MercadoPago.Resource.Customer;

namespace Decimatio.Infraestructure.Contracts
{
    public interface IMercadoPagoService
    {
        Task<IList<Customer>> SearchCliente();
        Task<IList<CustomerCard>> GetTarjetasCliente();
        Task<bool> CrearClientePago();
        Task<PreferenceResponse> CrearSolicitudPago(PreferenceData data);
    }
}

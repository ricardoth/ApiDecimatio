using Decimatio.Infraestructure.Models;
using MercadoPago.Client;
using MercadoPago.Client.Customer;
using MercadoPago.Client.Preference;
using MercadoPago.Resource;
using MercadoPago.Resource.Customer;
using MercadoPago.Resource.Preference;

namespace Decimatio.Infraestructure.Services
{
    public sealed class MercadoPagoService : IMercadoPagoService
    {
        public MercadoPagoService()
        {
                
        }

        public async Task<bool> CrearClientePago()
        {
            try
            {
                var customerRequest = new CustomerRequest
                {
                    Email = "asd@gmail.com",
                };
                var customerClient = new CustomerClient();
                Customer customer = await customerClient.CreateAsync(customerRequest);


                var cardRequest = new CustomerCardCreateRequest
                {
                    Token = "9b2d63e00d66a8c721607214cedaecda"
                };
                CustomerCard card = await customerClient.CreateCardAsync(customer.Id, cardRequest);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear el cliente: {ex.Message}");
                return false;
            }
        }

        public async Task<IList<Customer>> SearchCliente()
        {
            try
            {
                var customerClient = new CustomerClient();
                var search = new SearchRequest
                {
                    Filters = new Dictionary<string, object>
                    {
                        ["email"] = "ricardotilleriaochoa@gmail.com"
                    }
                };
                ResultsResourcesPage<Customer> results = await customerClient.SearchAsync(search);
                return results.Results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un error al traer el cliente {ex.Message}");
            }
        }

        public async Task<IList<CustomerCard>> GetTarjetasCliente()
        {
            try
            {
                var customerClient = new CustomerClient();
                ResourcesList<CustomerCard> cards = await customerClient.ListCardsAsync("1603079596-QCIyJ8LMDzXFT6");
                return cards;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un error al traer el tarjeta {ex.Message}");
            }
            
        }
             
        public async Task<PreferenceResponse> CrearSolicitudPago(PreferenceData data)
        {
            try
            {
                var preferenceRequest = new PreferenceRequest
                {
                    Items = new List<PreferenceItemRequest>
                    {
                        new PreferenceItemRequest
                        {
                            Title = data.Description,
                            UnitPrice = data.Price,
                            Quantity = data.Quantity
                        }
                },
                    BackUrls = new PreferenceBackUrlsRequest
                    {
                        Success = "http://localhost:5173/successShop",
                        Failure = "http://localhost:5173/failureShop",
                        Pending = "http://localhost:5173/pendingShop"
                    },
                    AutoReturn = "approved",
                };

                var client = new PreferenceClient();
                Preference preference = await client.CreateAsync(preferenceRequest);
                //Insertar pago en la BD y retornar entidad personalizada
                PreferenceResponse preferenceResponse = new()
                {
                    Preference = preference,
                    Tickets = data.Tickets,
                };
                return preferenceResponse;
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Error al crear el pago: {ex.Message}");
            }
            
        }
    }
}
using Decimatio.Infraestructure.Models;
using MercadoPago.Client;
using MercadoPago.Client.Customer;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource;
using MercadoPago.Resource.Customer;
using MercadoPago.Resource.Preference;

namespace Decimatio.Infraestructure.Services
{
    public sealed class MercadoPagoService : IMercadoPagoService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly MercadoPagoOptions _mercadoPagoOptions;

        public MercadoPagoService(ITicketRepository ticketRepository, IOptions<MercadoPagoOptions> options)
        {
            _ticketRepository = ticketRepository;
            _mercadoPagoOptions = options.Value;
            MercadoPagoConfig.AccessToken = _mercadoPagoOptions.AccessToken;
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
             
        public async Task<Preference> CrearSolicitudPago(PreferenceData data)
        {
            try
            {
                string transactionId = Guid.NewGuid().ToString();
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
                        //Success = $"http://localhost:5173/successShop?transactionId={transactionId}",
                        //Failure = $"http://localhost:5173/failureShop?transactionId={transactionId}",
                        //Pending = $"http://localhost:5173/pendingShop?transactionId={transactionId}"

                        Success = $"https://resonancepasstickets.netlify.app/successShop?transactionId={transactionId}",
                        Failure = $"https://resonancepasstickets.netlify.app/failureShop?transactionId={transactionId}",
                        Pending = $"https://resonancepasstickets.netlify.app/pendingShop?transactionId={transactionId}"
                    },
                    AutoReturn = "approved",
                    
                };

                var client = new PreferenceClient();
                Preference preference = await client.CreateAsync(preferenceRequest);

                foreach (var item in data.Tickets)
                {
                    var preferenceTicket = new PreferenceTicket()
                    {
                        PreferenceCode = preference.Id,
                        TransactionId = transactionId,
                        IdUsuario = (int)item.IdUsuario,
                        IdEvento = (int)item.IdEvento,
                        IdSector = (int)item.IdSector,
                        IdMedioPago = (int)item.IdMedioPago,
                        MontoPago = item.MontoPago,
                        MontoTotal = item.MontoTotal,
                        FechaTicket = item.FechaTicket,
                        Activo = item.Activo
                    };

                    var result = await _ticketRepository.AddPreferenceTicket(preferenceTicket);

                    if (!result) {
                        // registrar en log (insights) los posibles ticket con errores/no insertados
                        throw new BadRequestException($"Error al registrar el pago {transactionId}");
                    }
                }

                return preference;
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Error al crear el pago: {ex.Message}");
            }
            
        }
    }
}
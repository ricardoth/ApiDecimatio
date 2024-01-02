using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Decimatio.Infraestructure.Services
{
    internal sealed class PayPalService : IPayPalService
    {
        private readonly string _clientId = "AT4w7-G38w84HOFF_FrAMsfoM3_59X-730stJupYzgEaP0pAVyC7NZf8jh1OqaDu3CXKvJoGPoo6gf70";
        private readonly string _secretKey = "EMQa6WqBSR8LgDmKcQrHw0XditfCcSWnE_Xk86m_19wwx9L1fhOLw4chaZlkNZtqjPKjvgssByq0iWxI";

        public async Task<string> CreateAccessToken()
        {
            try
            {
                using var client = new HttpClient();
                var bytes = Encoding.UTF8.GetBytes($"{_clientId}:{_secretKey}");
                var base64 = Convert.ToBase64String(bytes);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);
                var response = await client.PostAsync("https://api.sandbox.paypal.com/v1/oauth2/token", new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded"));
                var content = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<dynamic>(content);
                return token.access_token;
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"No se pudo generar el token de acceso a PayPal {ex.Message}");
            }
        }

        public async Task<string> CreatePayment()
        {
            var accessToken = await CreateAccessToken();

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var payment = new OrderPayPal()
            { 
                intent = "CAPTURE",
                purchase_units = new List<PurchaseUnit>() 
                {
                    new PurchaseUnit() 
                    { 
                        items = new List<Item>()
                        { 
                            new Item() 
                            {
                                name = "T-Shirt",
                                description = "GreenXL",
                                quantity = "1",
                                unit_amount = new UnitAmount()
                                {
                                    currency_code = "USD",
                                    value = "100.00"
                                }
                            }
                        },
                        amount = new Amount()
                        {
                            currency_code = "USD",
                            value = "100.00",
                            breakdown = new Breakdown() 
                            {
                                item_total = new ItemTotal()
                                {
                                    currency_code = "USD",
                                    value = "100.00"
                                }
                            }
                        }
                    }
                },
                application_context = new ApplicationContext()
                {
                    return_url = "https://resonancepasstickets.netlify.app",
                    cancel_url = "https://resonancepasstickets.netlify.app",
                }
            };

            var json = JsonConvert.SerializeObject(payment);
            var response = await client.PostAsync("https://api.sandbox.paypal.com/v2/checkout/orders", new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            //var order = JsonConvert.DeserializeObject<dynamic>(content);

            return content;
        }
    }
}


public class OrderPayPal 
{
    public string intent { get; set; }
    public List<PurchaseUnit> purchase_units { get; set; }
    public ApplicationContext application_context { get; set; }
}

public class PurchaseUnit 
{
    public List<Item> items { get; set; }
    public Amount amount { get; set; }
}

public class Item 
{
    public string name { get; set; }
    public string description { get; set; }
    public string quantity { get; set; }
    public UnitAmount unit_amount { get; set; }
}

public class UnitAmount
{
    public string currency_code { get; set; }
    public string value { get; set; }
}

public class Amount
{ 
    public string currency_code { get; set;}
    public string value { get; set;}
    public Breakdown breakdown { get; set;}
}

public class Breakdown 
{
    public ItemTotal item_total { get; set; }
}

public class ItemTotal 
{
    public string currency_code { get; set; }
    public string value { get; set; }
}

public class ApplicationContext
{
    public string return_url { get; set; }
    public string cancel_url { get; set; }
}
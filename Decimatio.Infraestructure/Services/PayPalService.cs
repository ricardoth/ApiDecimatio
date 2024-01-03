using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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

        public async Task<PaymentResponse> CreatePayment(Order order)
        {
            var accessToken = await CreateAccessToken();

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = JsonConvert.SerializeObject(order);
            var response = await client.PostAsync("https://api.sandbox.paypal.com/v2/checkout/orders", new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaymentResponse>(content);

            return result;
        }
    }
}
using Decimatio.Common.DTOs;
using Decimatio.Domain.ValueObjects;
using Flurl.Http;

namespace Decimatio.Common.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailSenderOptions _emailSenderOptions;

        public EmailSenderService(EmailSenderOptions emailSenderOptions)
        {
            _emailSenderOptions = emailSenderOptions;       
        }

        private IFlurlRequest Url
        {
            get
            {
                string credentials = $"{_emailSenderOptions.UserBasicAuth}:{_emailSenderOptions.PassBasicAuth}";
                string encodingCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
                return _emailSenderOptions.Url.WithHeader("Authorization", $"Basic {encodingCredentials}");
            }
        }

        public async Task<string> SendEmailTicket(RequestEmailTicketDto emailTicketDto)
        {
            try
            {
                var request = await Url.AppendPathSegments("Email", "generateTicket")
                    .AllowHttpStatus()
                    .PostJsonAsync(emailTicketDto);

                if (!request.ResponseMessage.IsSuccessStatusCode)
                    throw new Exception("No se pudo enviar la solicitud a API Email Sender");

                var response = await request.GetStringAsync();
                return response;
            }
            catch (FlurlHttpException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

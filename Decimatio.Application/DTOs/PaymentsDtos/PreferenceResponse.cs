using Decimatio.Application.DTOs;
using MercadoPago.Resource.Preference;

namespace Decimatio.Infraestructure.Models
{
    public class PreferenceResponse
    {
        public Preference Preference { get; set; }
        public List<TicketDto> Tickets { get; set; }
    }
}

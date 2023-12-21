namespace Decimatio.Infraestructure.Models
{
    public class PreferenceData
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<TicketDto> Tickets { get; set; }
    }
}

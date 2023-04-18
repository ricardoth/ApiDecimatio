namespace Decimatio.Domain.Entities
{
    public class EventoBanda
    {
        public long IdEventoBanda { get; set; }
        public long IdEvento { get; set; }
        public long IdBanda { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Banda Banda { get; set; }
        public Evento Evento { get; set; }
    }
}

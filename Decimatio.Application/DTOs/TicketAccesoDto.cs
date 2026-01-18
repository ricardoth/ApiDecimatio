namespace Decimatio.Application.DTOs
{
    public record TicketAccesoDto
    {
        public long IdTicket { get; set; }
        public int IdEvento { get; set; }
        public int Rut { get; set; }
        public string Dv { get; set; }
        public string Correo { get; set; }
        public bool EsExtranjero { get; set; }
    }
}

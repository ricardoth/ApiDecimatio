namespace Decimatio.Domain.DTOs
{
    public class TicketInfoDto
    {
        public long IdTicket { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaTicket { get; set; }
        public string? RutUsuario { get; set; }
        public string? Nombres { get; set; }
        public string? ApellidoP { get; set; }
        public string? ApellidoM { get; set; }
        public string? Correo { get; set; }
    }
}

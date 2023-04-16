namespace Decimatio.Domain.Entities
{
    public class Ticket
    {
        public int IdTicket { get; set; }
        public int IdUsuario { get; set; }
        public int IdEvento { get; set; }
        public int IdSector { get; set; }
        public int IdMedioPago { get; set; }
        public decimal MontoPago { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaTicket { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; } 
        public DateTime FechaModified { get; set; }
    }
}

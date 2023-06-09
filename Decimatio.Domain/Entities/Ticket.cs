﻿namespace Decimatio.Domain.Entities
{
    public class Ticket
    {
        public long IdTicket { get; set; }
        public long IdUsuario { get; set; }
        public long IdEvento { get; set; }
        public long IdSector { get; set; }
        public long IdMedioPago { get; set; }
        public decimal MontoPago { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaTicket { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModified { get; set; }

        public Usuario Usuario { get; set; }
        public Evento Evento { get; set; }
        public Sector Sector { get; set; }
        public MedioPago MedioPago { get; set; }
    }
}

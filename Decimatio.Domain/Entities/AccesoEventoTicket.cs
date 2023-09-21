namespace Decimatio.Domain.Entities
{
    public class AccesoEventoTicket
    {
        public long IdAccesoEvento { get; set; }
        public long IdTicket { get; set; }
        public int IdUsuario  { get; set; }
        public int Rut  { get; set; }
        public string Dv  { get; set; }
        public string Nombres  { get; set; }
        public string ApellidoP  { get; set; }
        public string ApellidoM  { get; set; }
        public int IdEstadoTicket  { get; set; }
        public string EstadoTicket  { get; set; }
        public DateTime? FechaHoraEntrada { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
    }
}

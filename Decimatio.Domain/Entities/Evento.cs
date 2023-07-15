namespace Decimatio.Domain.Entities
{
    public class Evento
    {
        public long? IdEvento { get; set; }
        public int? IdLugar { get; set; }
        public string? NombreEvento { get; set; }
        public string? Direccion { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Flyer { get; set; }
        public bool? Activo { get; set; }
        public Lugar? Lugar { get; set; }
    }
}

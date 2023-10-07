namespace Decimatio.Domain.DTOs
{
    public class EventoPostDto
    {
        public long? IdEvento { get; set; }
        public int? IdLugar { get; set; }
        public string? NombreEvento { get; set; }
        public string? Direccion { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Flyer { get; set; }
        public string? ContenidoFlyer { get; set; }
        public bool? Activo { get; set; }
    }
}

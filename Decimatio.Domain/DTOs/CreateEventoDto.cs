namespace Decimatio.Domain.DTOs
{
    public class CreateEventoDto
    {
        public int? IdLugar { get; set; }
        public string? NombreEvento { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Flyer { get; set; }
        public string? ContenidoFlyer { get; set; }
        public string? Observacion { get; set; }
        public string? ProductoraResponsable { get; set; }
        public bool? Banner { get; set; }
        public string? ContenidoBanner { get; set; }
        public bool? Activo { get; set; }
    }
}

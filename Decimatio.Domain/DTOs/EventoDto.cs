namespace Decimatio.Domain.DTOs
{
    public class EventoDto
    {
        public long IdEvento { get; set; }
        public int IdLugar { get; set; }
        public string NombreEvento { get; set; }
        public string? Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string? Flyer { get; set; }
        public string? UrlImagenFlyer { get; set; }
        public string? Observacion { get; set; }
        public string? ProductoraResponsable { get; set; }
        public bool? Banner { get; set; }
        public string? ContenidoBanner { get; set; }
        public bool? Activo { get; set; }
        public LugarDto? Lugar { get; set; }
    }
}

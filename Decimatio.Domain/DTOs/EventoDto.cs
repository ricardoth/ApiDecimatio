namespace Decimatio.Domain.DTOs
{
    public class EventoDto
    {
        public long IdEvento { get; set; }
        public string NombreEvento { get; set; }
        public string Direccion { get; set; }
        public DateTime Fecha { get; set; }
        public string? Flyer { get; set; }
        public LugarDto Lugar { get; set; }
    }
}

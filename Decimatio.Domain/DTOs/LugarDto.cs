namespace Decimatio.Domain.DTOs
{
    public class LugarDto
    {
        public int IdLugar { get; set; }
        public int IdComuna { get; set; }
        public string? NombreLugar { get; set; }
        public string? Ubicacion { get; set; }
        public string? Numeracion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? MapaReferencial { get; set; }
        public string? NombreMapaReferencial { get; set; }
        public bool Activo { get; set; }
        public ComunaDto? Comuna { get; set; }   
    }
}

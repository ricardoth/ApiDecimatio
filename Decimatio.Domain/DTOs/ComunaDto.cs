namespace Decimatio.Domain.DTOs
{
    public class ComunaDto
    {
        public int IdComuna { get; set; }
        public int IdRegion { get; set; }
        public string? NombreComuna { get; set; }
        public bool Activo { get; set; }
        public RegionDto? Region { get; set; }
    }
}

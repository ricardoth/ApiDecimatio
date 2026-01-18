namespace Decimatio.Application.DTOs
{
    public record RegionDto
    {
        public int IdRegion { get; set; }
        public int NumeroRegion { get; set; }
        public string? Abreviatura { get; set; }
        public string? NombreRegion { get; set; }
        public bool Activo { get; set; }
    }
}

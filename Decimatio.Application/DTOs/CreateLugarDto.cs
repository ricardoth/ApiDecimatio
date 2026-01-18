namespace Decimatio.Application.DTOs
{
    public record CreateLugarDto
    {
        public int IdComuna { get; set; }
        public string NombreLugar { get; set; }
        public string Ubicacion { get; set; }
        public string Numeracion { get; set; }
        public string? MapaReferencial { get; set; }
        public string? Base64ImagenMapaReferencial { get; set; }
        public bool Activo { get; set; }
    }
}

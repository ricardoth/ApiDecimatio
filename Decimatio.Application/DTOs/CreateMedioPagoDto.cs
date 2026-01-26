namespace Decimatio.Application.DTOs
{
    public record CreateMedioPagoDto
    {
        public string? NombreMedioPago { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImageBlob { get; set; }
        public bool? Activo { get; set; }
    }
}

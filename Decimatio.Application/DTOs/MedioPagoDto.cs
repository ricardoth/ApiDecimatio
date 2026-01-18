namespace Decimatio.Application.DTOs
{
    public record MedioPagoDto
    {
        public long IdMedioPago { get; set; }
        public string NombreMedioPago { get; set; }
        public string Descripcion { get; set; }
        public string? UrlImageBlob { get; set; }
        public string? NombreImageBlob { get; set; }
        public bool? Activo { get; set; }
    }
}

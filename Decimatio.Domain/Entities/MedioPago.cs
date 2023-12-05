namespace Decimatio.Domain.Entities
{
    public class MedioPago
    {
        public long IdMedioPago { get; set; }
        public string NombreMedioPago { get; set; }
        public string Descripcion { get; set; }
        public string? UrlImageBlob { get; set; }
        public bool Activo { get; set; }
    }
}

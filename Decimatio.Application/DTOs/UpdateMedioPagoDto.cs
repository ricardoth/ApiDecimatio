namespace Decimatio.Application.DTOs
{
    public record UpdateMedioPagoDto : CreateMedioPagoDto
    {
        public int? IdMedioPago { get; set; }
    }
}

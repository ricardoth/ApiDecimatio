namespace Decimatio.Domain.DTOs
{
    public class CreateSectorDto
    {
        public int IdEvento { get; set; }
        public string? NombreSector { get; set; }
        public int CapacidadDisponible { get; set; }
        public int CapacidadActual { get; set; }
        public int CapacidadTotal { get; set; }
        public decimal Precio { get; set; }
        public decimal? Cargo { get; set; }
        public decimal? Total { get; set; }
        public string? ColorHexa { get; set; }
        public bool Activo { get; set; }
    }
}

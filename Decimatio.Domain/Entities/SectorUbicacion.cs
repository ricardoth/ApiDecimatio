namespace Decimatio.Domain.Entities
{
    public class SectorUbicacion
    {
        public long? IdSectorUbicacion { get; set; }
        public long? IdSector { get; set; }
        public int? NumeroAsiento { get; set; }
        public string? Fila { get; set; }
        public bool? Disponible { get; set; }
        public bool? Activo { get; set; }
    }
}

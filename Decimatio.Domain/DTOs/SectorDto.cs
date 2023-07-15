namespace Decimatio.Domain.DTOs
{
    public class SectorDto
    {
        public long IdSector { get; set; }
        public string? NombreSector { get; set; }
        public EventoDto? Evento { get; set; }
    }
}

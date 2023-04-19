namespace Decimatio.Domain.Entities
{
    public class Sector
    {
        public long IdSector { get; set; }
        public int IdLugar { get; set; }
        public string NombreSector { get; set; }
        public int Capacidad { get; set; }
        public bool Activo { get; set; }

        public Lugar Lugar { get; set; }
    }
}

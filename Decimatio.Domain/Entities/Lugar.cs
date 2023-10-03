namespace Decimatio.Domain.Entities
{
    public class Lugar
    {
        public int IdLugar { get; set; }
        public string NombreLugar { get; set; }
        public string Ubicacion { get; set; }
        public string Numeracion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
        public Comuna Comuna { get; set; }  
    }
}

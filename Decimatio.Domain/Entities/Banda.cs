namespace Decimatio.Domain.Entities
{
    public class Banda
    {
        public long IdBanda { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public DateTime FormadoEn { get; set; }
        public bool Activo { get; set; }
    }
}

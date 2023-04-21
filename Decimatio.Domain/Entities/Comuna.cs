namespace Decimatio.Domain.Entities
{
    public class Comuna
    {
        public int IdComuna { get; set; }
        public int IdRegion { get; set; }
        public string NombreComuna { get; set; }
        public bool Activo { get; set; }
        public Region Region { get; set; }  
    }
}

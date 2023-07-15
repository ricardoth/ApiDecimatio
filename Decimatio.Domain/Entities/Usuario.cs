namespace Decimatio.Domain.Entities
{
    public class Usuario
    {
        public long IdUsuario { get; set; }
        public short IdTipoUsuario { get; set; }
        public int Rut { get; set; }
        public string? DV { get; set; }
        public string? Nombres { get; set; }
        public string? ApellidoP { get; set; }
        public string? ApellidoM { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}

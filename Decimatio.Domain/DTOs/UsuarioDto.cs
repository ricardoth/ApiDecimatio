namespace Decimatio.Domain.DTOs
{
    public class UsuarioDto
    {
        public long IdUsuario { get; set; }
        public int IdTipoUsuario { get; set; }
        public int Rut { get; set; }
        public string? DV { get; set; }
        public string? Nombres { get; set; }
        public string? ApellidoP { get; set; }
        public string? ApellidoM { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }
        public bool? Activo { get; set; }
        public TipoUsuarioDto? TipoUsuario { get; set; }
    }
}

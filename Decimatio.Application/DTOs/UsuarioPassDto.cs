namespace Decimatio.Application.DTOs
{
    public record UsuarioPassDto
    {
        public int IdUsuario { get; set; }
        public string Contrasena { get; set; }
        public string ConfirmarContrasena { get; set; }
    }
}

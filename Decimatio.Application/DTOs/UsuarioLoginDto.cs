namespace Decimatio.Application.DTOs
{
    public record UsuarioLoginDto
    {
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }
    }
}

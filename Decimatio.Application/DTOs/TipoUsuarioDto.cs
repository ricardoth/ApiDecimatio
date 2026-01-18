namespace Decimatio.Application.DTOs
{
    public record TipoUsuarioDto
    {
        public int IdTipoUsuario { get; set; }
        public string NombreTipoUsuario { get; set; }
        public bool Activo { get; set; }
    }
}

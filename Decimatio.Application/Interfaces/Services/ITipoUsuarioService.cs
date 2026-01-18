namespace Decimatio.Application.Interfaces.Services
{
    public interface ITipoUsuarioService
    {
        Task<IEnumerable<TipoUsuario>> GetAllTiposUsuarios();
    }
}

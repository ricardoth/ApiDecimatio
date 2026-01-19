namespace Decimatio.Application.Interfaces.Repositories
{
    public interface ITipoUsuarioRepository
    {
        Task<IEnumerable<TipoUsuario>> GetAllTipoUsuarios();
    }
}

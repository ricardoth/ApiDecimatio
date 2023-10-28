namespace Decimatio.Domain.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        Task<IEnumerable<TipoUsuario>> GetAllTipoUsuarios();
    }
}

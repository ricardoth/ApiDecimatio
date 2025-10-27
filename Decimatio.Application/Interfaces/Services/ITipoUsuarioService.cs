namespace Decimatio.Domain.Interfaces
{
    public interface ITipoUsuarioService
    {
        Task<IEnumerable<TipoUsuario>> GetAllTiposUsuarios();
    }
}

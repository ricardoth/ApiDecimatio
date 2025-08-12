namespace Decimatio.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllUsersPaginated(UsuarioQueryFilter filtros);
        Task<Usuario> GetById(long idUsuario);
        Task<Usuario> GetByRutDv(string rutDv);
        Task<Usuario> GetByCorreo(Usuario usuario);
        Task AddUsuario(Usuario usuario);
        Task<bool> UpdateUsuario(Usuario usuario);
        Task<bool> DeleteUsuario(long idUsuario);
        Task<Usuario> Login(Usuario usuario);
        Task<bool> ChangePassword(UsuarioPass usuario);
    }
}

namespace Decimatio.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<PagedList<Usuario>> GetAllUsers(UsuarioQueryFilter filtros);
        Task<IEnumerable<Usuario>> GetAllUsersFilter(string filtro);
        Task<Usuario> GetById(long idUsuario);
        Task AddUsuario(Usuario usuario);
        Task<bool> UpdateUsuario(Usuario usuario);
        Task<bool> DeleteUsuario(long idUsuario);
        Task<Usuario> Login(Usuario usuario);
        Task<bool> ChangePassword(string contrasena, string confirmContrasena);
    }
}

namespace Decimatio.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllUsers();
        Task<IEnumerable<Usuario>> GetAllUsersFilter(string filtro);
        Task<Usuario> GetById(int idUsuario);
        Task AddUsuario(Usuario usuario);
        Task<bool> UpdateUsuario(Usuario usuario);
        Task<bool> DeleteUsuario(int idUsuario);
    }
}

namespace Decimatio.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllUsers();
        Task<IEnumerable<Usuario>> GetAllUsersFilter(string filtro);
        Task<Usuario> GetById(int idUsuario);
    }
}

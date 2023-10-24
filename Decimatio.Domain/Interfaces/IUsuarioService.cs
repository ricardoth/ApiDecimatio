namespace Decimatio.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<PagedList<Usuario>> GetAllUsers(UsuarioQueryFilter filtros);
        Task<IEnumerable<Usuario>> GetAllUsersFilter(string filtro);
        Task<Usuario> GetById(int idUsuario);
    }
}

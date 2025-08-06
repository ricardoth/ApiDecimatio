using Decimatio.Domain.DTOs;

namespace Decimatio.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<PagedList<Usuario>> GetAllUsers(UsuarioQueryFilter filtros);
        Task<IEnumerable<Usuario>> GetAllUsersFilter(string filtro);
        Task<Usuario> GetById(long idUsuario);
        Task AddUsuario(CreateUsuarioDto createUsuarioDto);
        Task<bool> UpdateUsuario(Usuario usuario);
        Task<bool> DeleteUsuario(long idUsuario);
        Task<Usuario> Login(Usuario usuario);
        Task<bool> ChangePassword(UsuarioPass usuarioDto);
    }
}

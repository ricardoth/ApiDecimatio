using Decimatio.Domain.DTOs;

namespace Decimatio.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<PagedList<UsuarioDto>> GetAllUsers(UsuarioQueryFilter filtros);
        Task<IEnumerable<UsuarioDto>> GetAllUsersFilter(string filtro);
        Task<UsuarioDto> GetById(long idUsuario);
        Task AddUsuario(CreateUsuarioDto createUsuarioDto);
        Task<bool> UpdateUsuario(UpdateUsuarioDto updateUsuarioDto);
        Task<bool> DeleteUsuario(long idUsuario);
        Task<Usuario> Login(UsuarioLoginDto usuarioLoginDto);
        Task<bool> ChangePassword(UsuarioPass usuarioDto);
    }
}

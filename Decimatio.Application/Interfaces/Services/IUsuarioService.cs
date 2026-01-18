using Decimatio.Application.DTOs;
using Decimatio.Domain.CustomEntities;
using Decimatio.Domain.QueryFilters;

namespace Decimatio.Application.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<PagedList<UsuarioDto>> GetAllUsers(UsuarioQueryFilter filtros);
        Task<IEnumerable<UsuarioDto>> GetAllUsersFilter(string filtro);
        Task<UsuarioDto> GetById(long idUsuario);
        Task AddUsuario(CreateUsuarioDto createUsuarioDto);
        Task<bool> UpdateUsuario(UpdateUsuarioDto updateUsuarioDto);
        Task<bool> DeleteUsuario(long idUsuario);
        Task<UsuarioDto> Login(UsuarioLoginDto usuarioLoginDto);
        Task<bool> ChangePassword(UsuarioPassDto usuarioDto);
    }
}

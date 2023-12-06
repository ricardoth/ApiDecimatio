using Decimatio.Domain.Exceptions;

namespace Decimatio.Infraestructure.Services
{
    internal sealed class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly PaginationOptions _paginationOptions;

        public UsuarioService(IUsuarioRepository usuarioRepository, IOptions<PaginationOptions> paginationOptions) 
        {
            _usuarioRepository = usuarioRepository;
            _paginationOptions = paginationOptions.Value;
        }

        public async Task<PagedList<Usuario>> GetAllUsers(UsuarioQueryFilter filtros)
        {
            filtros.PageNumber = filtros.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filtros.PageNumber;
            filtros.PageSize = filtros.PageSize == 0 ? _paginationOptions.DefaultPageSize : filtros.PageSize;

            try
            {
                var usuarios = await _usuarioRepository.GetAllUsers();

                if (filtros.IdUsuario > 0)
                    usuarios = usuarios.Where(x => x.IdUsuario == filtros.IdUsuario);

                var pagedUsuarios = PagedList<Usuario>.Create(usuarios, filtros.PageNumber, filtros.PageSize);
                return pagedUsuarios;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un error en UsuarioService {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Usuario>> GetAllUsersFilter(string filtro)
        {
            try
            {
                var result = await _usuarioRepository.GetAllUsersFilter(filtro);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Usuario> GetById(long idUsuario)
        {
            try
            {
                var result = await _usuarioRepository.GetById(idUsuario);
                return result;  
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            try
            {
                var result = await _usuarioRepository.UpdateUsuario(usuario);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AddUsuario(Usuario usuario)
        {
           
            var rutDv = usuario.Rut + usuario.DV;
            var user = await _usuarioRepository.GetByRutDv(rutDv);
            if (user == null)
            {
                await _usuarioRepository.AddUsuario(usuario);
            }
            else {
                throw new BadRequestException("El Usuario ya existe en nuestros registros");
            }
        }

        public async Task<bool> DeleteUsuario(long idUsuario)
        {
            try
            {
                var result = await _usuarioRepository.DeleteUsuario(idUsuario);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Usuario> Login(Usuario usuario)
        {
            var result = await _usuarioRepository.Login(usuario);
            if (result is null)
            {
                throw new BadRequestException("El Usuario no existe en nuestros registros, por favor registrese");
            }
            return result;
        }
    }
}

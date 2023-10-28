namespace Decimatio.Infraestructure.Services
{
    public class UsuarioService : IUsuarioService
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

        public async Task<Usuario> GetById(int idUsuario)
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
            try
            {
                await _usuarioRepository.AddUsuario(usuario);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteUsuario(int idUsuario)
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
    }
}

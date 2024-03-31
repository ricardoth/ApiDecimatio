namespace Decimatio.Infraestructure.Services
{
    internal sealed class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly PaginationOptions _paginationOptions;
        private readonly IValidator<Usuario> _validator;
        private readonly IPasswordService _passwordService;

        public UsuarioService(IUsuarioRepository usuarioRepository, 
            IOptions<PaginationOptions> paginationOptions, 
            IValidator<Usuario> validator,
            IPasswordService passwordService) 
        {
            _usuarioRepository = usuarioRepository;
            _paginationOptions = paginationOptions.Value;
            _validator = validator;
            _passwordService = passwordService;
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
                throw new BadRequestException($"Ha ocurrido un error al actualizar el usuario: {ex.Message}");
            }
        }

        public async Task AddUsuario(Usuario usuario)
        {
            var validationResult = _validator.Validate(usuario);
            if (!validationResult.IsValid) {
                var errores = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationResultException(errores);
            }

            var rutDv = usuario.Rut + usuario.DV;
            Usuario? user = null; 

            if (!(bool)usuario.EsExtranjero)
                user = await _usuarioRepository.GetByRutDv(rutDv);
            else 
                user = await _usuarioRepository.GetByCorreo(usuario.Correo);

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
                throw new BadRequestException($"Ha ocurrido un error al eliminar el usuario: {ex.Message}");
            }
        }

        public async Task<Usuario> Login(Usuario usuario)
        {
            var result = await _usuarioRepository.Login(usuario);
            if (result is null)
                throw new BadRequestException("El Usuario no existe en nuestros registros, por favor registrese");
            else {
                var isValid = _passwordService.Check(result.Contrasena, usuario.Contrasena);
                if (isValid)
                    return result;
                else
                    throw new BadRequestException("La contraseña es incorrecta, por favor verifique");
            }
        }

        public async Task<bool> ChangePassword(UsuarioPass usuario)
        {
            if (usuario.Contrasena == null || usuario.Contrasena == "")
                throw new BadRequestException("La contraseña no es válida");

            var userExists = await GetById(usuario.IdUsuario);
            if (userExists == null)
                throw new BadRequestException("El Usuario no existe en nuestros registros");

            usuario.Contrasena = _passwordService.Hash(usuario.Contrasena);

            var comparedPass = _passwordService.Check(usuario.Contrasena, usuario.ConfirmarContrasena);
            if (!comparedPass)
                throw new BadRequestException("Las contraseñas no coinciden, por favor verifique");

            var result = await _usuarioRepository.ChangePassword(usuario);
            if (!result)
                throw new BadRequestException("No se pudo actualizar la contraseña");

            return result;
        }
    }
}

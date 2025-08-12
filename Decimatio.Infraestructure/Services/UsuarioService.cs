namespace Decimatio.Infraestructure.Services
{
    internal sealed class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRepository _genericRepository;
        private readonly PaginationOptions _paginationOptions;
        private readonly IValidator<CreateUsuarioDto> _createUsuarioValidator;
        private readonly IValidator<UpdateUsuarioDto> _updateUsuarioValidator;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, 
            IRepository genericRepository,
            IOptions<PaginationOptions> paginationOptions,
            IValidator<CreateUsuarioDto> createUsuarioValidator,
            IValidator<UpdateUsuarioDto> updateUsuarioValidator,
            IPasswordService passwordService,
            IMapper mapper) 
        {
            _usuarioRepository = usuarioRepository;
            _genericRepository = genericRepository;
            _paginationOptions = paginationOptions.Value;
            _createUsuarioValidator = createUsuarioValidator;
            _updateUsuarioValidator = updateUsuarioValidator;   
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public async Task<PagedList<UsuarioDto>> GetAllUsers(UsuarioQueryFilter filtros)
        {
            filtros.PageNumber = filtros.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filtros.PageNumber;
            filtros.PageSize = filtros.PageSize == 0 ? _paginationOptions.DefaultPageSize : filtros.PageSize;

            var usuariosPaginated = await _usuarioRepository.GetAllUsersPaginated(filtros);
            if (!usuariosPaginated.Any())
                throw new NoContentException();

            var usuarios = _mapper.Map<IEnumerable<UsuarioDto>>(usuariosPaginated);
            var totalCount = await _genericRepository.GetTotalCount("Usuario");
            var pagedUsuarios = PagedList<UsuarioDto>.CreatePaginationFromDb(usuarios, totalCount, filtros.PageNumber, filtros.PageSize);
            return pagedUsuarios;
            
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllUsersFilter(string filtro)
        {
            UsuarioQueryFilter filters = new UsuarioQueryFilter()
            {
                PageNumber = 1,
                PageSize = 100,
                Query = filtro
            };

            var usuariosFiltered = await _usuarioRepository.GetAllUsersPaginated(filters);
            if (!usuariosFiltered.Any())
                throw new NoContentException();

            var usuarios = _mapper.Map<IEnumerable<UsuarioDto>>(usuariosFiltered);
            return usuarios;

        }

        public async Task<UsuarioDto> GetById(long idUsuario)
        {
            var result = await _usuarioRepository.GetById(idUsuario);
            if (result is null)
                throw new NotFoundException($"No se encontró el usuario en la bd");

            var usuario = _mapper.Map<UsuarioDto>(result);
            return usuario;  
        }

        public async Task<bool> UpdateUsuario(UpdateUsuarioDto updateUsuarioDto)
        {
            if (updateUsuarioDto.IdUsuario <= 0)
                throw new NotFoundException($"Usuario inválido");

            var validationResult = _updateUsuarioValidator.Validate(updateUsuarioDto);
            if (!validationResult.IsValid)
            {
                var errores = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationResultException(errores);
            }

            if (updateUsuarioDto.Contrasena is not null)
                updateUsuarioDto.Contrasena = _passwordService.Hash(updateUsuarioDto.Contrasena);

            var usuarioBD = await _usuarioRepository.GetById(updateUsuarioDto.IdUsuario);
            var usuario = _mapper.Map(updateUsuarioDto, usuarioBD);
            var result = await _usuarioRepository.UpdateUsuario(usuario);
            return result;
        }

        public async Task AddUsuario(CreateUsuarioDto createUsuarioDto)
        {
            var validationResult = _createUsuarioValidator.Validate(createUsuarioDto);
            if (!validationResult.IsValid) {
                var errores = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationResultException(errores);
            }

            if (createUsuarioDto.Contrasena is not null)
                createUsuarioDto.Contrasena = _passwordService.Hash(createUsuarioDto.Contrasena);

            var rutDv = createUsuarioDto.Rut + createUsuarioDto.DV;
            Usuario? user = null;
            var usuario = _mapper.Map<Usuario>(createUsuarioDto);

            if (!(bool)createUsuarioDto.EsExtranjero)
                user = await _usuarioRepository.GetByRutDv(rutDv);
            else 
                user = await _usuarioRepository.GetByCorreo(usuario);

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
            if (idUsuario <= 0)
                throw new NotFoundException("Usuario inválido");

            var usuarioBd = await _usuarioRepository.GetById(idUsuario);
            var result = await _usuarioRepository.DeleteUsuario(idUsuario);
            return result;
        }

        public async Task<UsuarioDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioLoginDto);
            var result = await _usuarioRepository.Login(usuario);
            if (result is null)
                throw new BadRequestException("El Usuario no existe en nuestros registros, por favor registrese");
            else {
                var isValid = _passwordService.Check(result.Contrasena, usuario.Contrasena);
                var usuarioMapped = _mapper.Map<UsuarioDto>(result);
                if (isValid)
                    return usuarioMapped;
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

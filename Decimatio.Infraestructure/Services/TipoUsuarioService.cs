namespace Decimatio.Infraestructure.Services
{
    internal sealed class TipoUsuarioService : ITipoUsuarioService
    {
        private readonly ITipoUsuarioRepository _tipoUsuarioRepository;

        public TipoUsuarioService(ITipoUsuarioRepository tipoUsuarioRepository)
        {
            _tipoUsuarioRepository = tipoUsuarioRepository;        
        }

        public async Task<IEnumerable<TipoUsuario>> GetAllTiposUsuarios()
        {
            try
            {
                return await _tipoUsuarioRepository.GetAllTipoUsuarios();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

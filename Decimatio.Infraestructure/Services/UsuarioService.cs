namespace Decimatio.Infraestructure.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository) 
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsers()
        {
            try
            {
                var result = await _usuarioRepository.GetAllUsers();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

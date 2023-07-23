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
    }
}

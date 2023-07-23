namespace Decimatio.Infraestructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDataBaseConnection _connection;
        public UsuarioRepository(IDataBaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsers()
        {
            var result = await _connection.GetListAsync<Usuario>("GET_USUARIOS", Queries.GET_USUARIOS);
            return result;
        }

        public async Task<Usuario> GetById(int idUsuario)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@IdUsuario", idUsuario}
            };
            var dynamicParam = new DynamicParameters(dictionary);
            var result = await _connection.FirstOrDefaultAsync<Usuario>("GET_USUARIO_ID", Queries.GET_USUARIO_ID, dynamicParam);
            return result;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsersFilter(string filtro)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@Filtro", filtro }
            };

            var dynamicParam = new DynamicParameters(dictionary);
            var result = await _connection.GetListAsync<Usuario>("GET_USUARIOS_FILTRO", Queries.GET_USUARIOS_FILTRO, dynamicParam);
            return result;
        }
    }
}

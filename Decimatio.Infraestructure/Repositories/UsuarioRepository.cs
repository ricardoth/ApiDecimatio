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
            var result = await _connection.GetListAsync<Usuario>("GET_USUARIOS", Querys.GET_USUARIOS);
            return result;
        }

        public async Task<Usuario> GetById(int idUsuario)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@IdUsuario", idUsuario}
            };
            var dynamicParam = new DynamicParameters(dictionary);
            var result = await _connection.FirstOrDefaultAsync<Usuario>("GET_USUARIO_ID", Querys.GET_USUARIO_ID, dynamicParam);
            return result;
        }
    }
}

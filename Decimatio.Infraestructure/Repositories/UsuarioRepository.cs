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
    }
}

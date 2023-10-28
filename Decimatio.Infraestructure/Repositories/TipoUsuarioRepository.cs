namespace Decimatio.Infraestructure.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly DataBaseConfig _connection;
        public TipoUsuarioRepository(DataBaseConfig connection)
        {
            _connection = connection;        
        }
        public async Task<IEnumerable<TipoUsuario>> GetAllTipoUsuarios()
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryAsync<TipoUsuario>(Queries.GET_TIPO_USUARIO);
        }
    }
}

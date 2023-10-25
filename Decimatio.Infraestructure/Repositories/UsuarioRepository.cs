﻿namespace Decimatio.Infraestructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataBaseConfig _connection;

        public UsuarioRepository(DataBaseConfig connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsers()
        {
           
            using var conn = new SqlConnection(_connection.ConnectionString);
            conn.Open();

            var result = (await conn.QueryAsync<Usuario, TipoUsuario, Usuario>(
                Queries.GET_USUARIOS,
                (user, tipo) => 
                {
                    user.TipoUsuario = tipo;
                    return user;
                },
                splitOn: "IdTipoUsuario"
                )).ToList();

            return result;
        }

        public async Task<Usuario> GetById(int idUsuario)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@IdUsuario", idUsuario}
            };
            var dynamicParam = new DynamicParameters(dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            conn.Open();
            return await conn.QueryFirstOrDefaultAsync<Usuario>(Queries.GET_USUARIO_ID, dynamicParam);
        }

        public async Task<IEnumerable<Usuario>> GetAllUsersFilter(string filtro)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@Filtro", filtro }
            };

            var dynamicParam = new DynamicParameters(dictionary);

            using var conn = new SqlConnection(_connection.ConnectionString);
            conn.Open();

            var result = (await conn.QueryAsync<Usuario>(
                Queries.GET_USUARIOS_FILTRO,
                dynamicParam
                )).Distinct().ToList();

            return result;
        }
    }
}

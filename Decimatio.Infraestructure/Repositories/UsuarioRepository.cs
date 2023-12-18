using Decimatio.Domain.Entities;

namespace Decimatio.Infraestructure.Repositories
{
    internal sealed class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataBaseConfig _connection;

        public UsuarioRepository(DataBaseConfig connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsers()
        {
            using var conn = new SqlConnection(_connection.ConnectionString);

            var result = (await conn.QueryAsync<Usuario, TipoUsuario, Usuario>(
                Querys.GET_USUARIOS,
                (user, tipo) => 
                {
                    user.TipoUsuario = tipo;
                    return user;
                },
                splitOn: "IdTipoUsuario"
                )).ToList();

            return result;
        }

        public async Task<Usuario> GetById(long idUsuario)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@IdUsuario", idUsuario}
            };
            var dynamicParam = new DynamicParameters(dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryFirstOrDefaultAsync<Usuario>(Querys.GET_USUARIO_ID, dynamicParam);
        }

        public async Task<IEnumerable<Usuario>> GetAllUsersFilter(string filtro)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@Filtro", filtro }
            };

            var dynamicParam = new DynamicParameters(dictionary);

            using var conn = new SqlConnection(_connection.ConnectionString);

            var result = (await conn.QueryAsync<Usuario>(
                Querys.GET_USUARIOS_FILTRO,
                dynamicParam
                )).Distinct().ToList();

            return result;
        }

        public async Task AddUsuario(Usuario usuario)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@IdTipoUsuario", usuario.IdTipoUsuario },
                { "@Rut", usuario.Rut },
                { "@Dv", usuario.DV },
                { "@Nombres", usuario.Nombres },
                { "@ApellidoP", usuario.ApellidoP },
                { "@ApellidoM", usuario.ApellidoM },
                { "@Direccion", usuario.Direccion },
                { "@Telefono", usuario.Telefono },
                { "@Correo", usuario.Correo },
                { "@Contrasena", usuario.Contrasena },
                { "@Activo", usuario.Activo },
            };

            var dynamicParam = new DynamicParameters(dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            await conn.QueryAsync(Querys.INSERT_USUARIO, dynamicParam);
        }

        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@IdUsuario", usuario.IdUsuario},
                { "@IdTipoUsuario", usuario.IdTipoUsuario },
                { "@Rut", usuario.Rut },
                { "@Dv", usuario.DV },
                { "@Nombres", usuario.Nombres },
                { "@ApellidoP", usuario.ApellidoP },
                { "@ApellidoM", usuario.ApellidoM },
                { "@Direccion", usuario.Direccion },
                { "@Telefono", usuario.Telefono },
                { "@Correo", usuario.Correo },
                { "@Activo", usuario.Activo },
            };

            var dynamicParam = new DynamicParameters(dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteScalarAsync<bool>(Querys.UPDATE_USUARIO, dynamicParam);
        }

        public async Task<bool> DeleteUsuario(long idUsuario)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@IdUsuario", idUsuario},
            };
            var dynamicParam = new DynamicParameters(dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteScalarAsync<bool>(Querys.DELETE_USUARIO, dynamicParam);
        }

        public async Task<Usuario> GetByRutDv(string rutDv)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@RutDv", rutDv }
            };

            var dynamicParam = new DynamicParameters(dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryFirstOrDefaultAsync<Usuario>(Querys.GET_USUARIO_RUT, dynamicParam);
        }

        public async Task<Usuario> Login(Usuario usuario)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@Correo", usuario.Correo },
            };

            var dynamicParam = new DynamicParameters(dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryFirstOrDefaultAsync<Usuario>(Querys.LOGIN_USUARIO, dynamicParam);
        }

        public async Task<bool> ChangePassword(string contrasena)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@Contrasena", contrasena },
            };

            var dynamicParam = new DynamicParameters(dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteScalarAsync<bool>(Querys.CAMBIAR_CONTRASENA, dynamicParam);
        }
    }
}

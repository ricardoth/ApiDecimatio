﻿namespace Decimatio.Infraestructure.Repositories
{
    public class SectorRepository : ISectorRepository
    {
        private readonly DataBaseConfig _connection;

        public SectorRepository(DataBaseConfig connection)
        {
            _connection = connection;       
        }

        public async Task<IEnumerable<Sector>> GetAllSectores()
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return (await conn.QueryAsync<Sector, Evento, Sector>(
                Queries.GET_SECTORES,
                (sector, evento) => {
                    sector.Evento = evento;
                    return sector;
                },
                splitOn: "IdEvento"
                )).ToList();
        }

        public async Task<IEnumerable<Sector>> GetSectoresByEvento(int idEvento)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return (await conn.QueryAsync<Sector, Evento, Sector>(Queries.GET_SECTORES_BY_EVENTO, 
                (sector, evento) => {
                    sector.Evento = evento;
                    return sector;
                },
                new { IdEvento = idEvento },
                splitOn: "IdEvento")).ToList();
        }

        public async Task<Sector> GetById(int idSector)
        {
            var dictionary = new Dictionary<string, object>
            {
                { "@IdSector", idSector },
            };

            var dynamicParam = new DynamicParameters( dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.QueryFirstOrDefaultAsync<Sector>(Queries.GET_SECTOR_ID, dynamicParam);
        }

        public async Task<int> AddSector(Sector sector)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteAsync(Queries.INSERT_SECTOR, sector);
        }

        public async Task<bool> UpdateSector(Sector sector)
        {
            var dictionary = new Dictionary<string, object>()
            {
                { "@IdSector", sector.IdSector },
                { "@IdEvento", sector.IdEvento },
                { "@NombreSector", sector.NombreSector },
                { "@CapacidadTotal", sector.CapacidadTotal },
                { "@CapacidadActual", sector.CapacidadActual },
                { "@CapacidadDisponible", sector.CapacidadDisponible },
                { "@Precio", sector.Precio },
                { "@Activo", sector.Activo },
            };

            var dynamicParam = new DynamicParameters(dictionary);
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteScalarAsync<bool>(Queries.UPDATE_SECTOR, dynamicParam);
        }

        public async Task<bool> DeleteSector(int idSector)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteScalarAsync<bool>(Queries.DELETE_SECTOR, new { IdSector = idSector });
        }
    }
}

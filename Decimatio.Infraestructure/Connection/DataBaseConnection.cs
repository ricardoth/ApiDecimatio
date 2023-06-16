namespace Decimatio.Infraestructure.Connection
{
    public class DataBaseConnection : IDataBaseConnection
    {
        private readonly DataBaseConfig _connection;
        private readonly Guid _key;
        public DataBaseConnection(DataBaseConfig connection) 
        { 
            _connection = connection;
            _key = Guid.NewGuid();
        }
        
        public async Task<int?> ExecuteAsync(string queryName, string query, object entity)
        {
            DateTime startTime = DateTime.Now;
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool isSuccess = true;

            try
            {
                using (var conn = new SqlConnection(_connection.ConnectionString))
                {
                    return await conn.ExecuteAsync(query, entity);
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                throw ex;
            }
            finally { stopwatch.Stop(); }
        }

        

        public async Task<long?> ExecuteScalar(string queryName, string query, object entity)
        {
            DateTime startTime = DateTime.Now;
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool isSuccess = true;

            try
            {
                using (var conn = new SqlConnection(_connection.ConnectionString))
                {
                    long newId = await conn.ExecuteScalarAsync<long>(query, entity);
                    return newId;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                throw ex;
            }
            finally { stopwatch.Stop(); }
        }

        public async Task<T?> ExecuteScalar<T>(string queryName, string query, object entity)
        {

            DateTime startTime = DateTime.Now;
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool isSuccess = true;

            try
            {
                using (var conn = new SqlConnection(_connection.ConnectionString))
                {
                    var newObject = await conn.ExecuteScalarAsync<T>(query, entity);
                    return newObject;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                throw ex;
            }
            finally { stopwatch.Stop(); }
        }

        public async Task<T> FirstOrDefaultAsync<T>(string queryName, string query, object entity)
        {
            var st = DateTime.Now;
            var w = Stopwatch.StartNew();
            var success = true;

            try
            {
                using var conn = new SqlConnection(_connection.ConnectionString);
                return await conn.QueryFirstOrDefaultAsync<T>(query, entity);
            }
            catch (Exception ex)
            {
                success = false;
                throw ex;
            }
            finally
            {
                w.Stop();
            }
        }

        public async Task<Ticket> FirstOrDefaultWithObjectAsync<T>(string queryName, string query, long tickedId)
        {
            var st = DateTime.Now;
            var w = Stopwatch.StartNew();
            var success = true;

            try
            {
                var ticketDictionary = new Dictionary<long, Ticket>();
                using var conn = new SqlConnection(_connection.ConnectionString);
                var tickets = (await conn.QueryAsync<Ticket, Usuario, Evento, Sector, MedioPago, Lugar, Comuna,Ticket>(
                    query,
                    (ticket, usuario, evento, sector, medioPago, lugar, comuna) =>
                    {
                        if (!ticketDictionary.TryGetValue(ticket.IdTicket, out var ticketEntry))
                        {
                            ticketEntry = ticket;
                            ticketDictionary.Add(ticketEntry.IdTicket, ticketEntry);

                        }

                        ticketEntry.Usuario = usuario;
                        ticketEntry.Evento = evento;
                        ticketEntry.Sector = sector;
                        ticketEntry.MedioPago = medioPago;
                        ticketEntry.Sector.Lugar = lugar;
                        ticketEntry.Sector.Lugar.Comuna = comuna;
                        return ticketEntry;
                    },
                    new { IdTicket = tickedId },
                    splitOn: "IdUsuario,IdEvento,IdSector,IdMedioPago,IdLugar,IdComuna"
                )).Distinct().ToList();

                var ticketResult = tickets.FirstOrDefault();

                return ticketResult;
            }
            catch (Exception ex)
            {
                success = false;
                throw ex;
            }
            finally
            {
                w.Stop();
            }
        }

        public async Task<IEnumerable<T>> GetListAsync<T>(string queryName, string query)
        {
            var st = DateTime.Now;
            var w = Stopwatch.StartNew();
            var success = true;

            try
            {
                using var conn = new SqlConnection(_connection.ConnectionString);
                return await conn.QueryAsync<T>(query);
            }
            catch (Exception ex)
            {
                success = false;
                throw ex;
            }
            finally
            {
                w.Stop();
            }

        }

        public async Task<T> QuerySingleAsync<T>(string queryName, string query, object entity)
        {
            var st = DateTime.Now;
            var w = Stopwatch.StartNew();
            var success = true;

            try
            {
                using var conn = new SqlConnection(_connection.ConnectionString);
                return await conn.QuerySingleAsync<T>(query, entity);
            }
            catch (Exception ex)
            {
                success = false;
                throw ex;
            }
            finally
            {
                w.Stop();
            }
        }

        public async Task<T> ExecuteAsync<T>(string queryName, string query, object entity)
        {
            var st = DateTime.Now;
            var w = Stopwatch.StartNew();
            var success = true;

            try
            {
                using var conn = new SqlConnection(_connection.ConnectionString);
                await conn.ExecuteAsync(query, entity);
                return (T)entity;
            }
            catch (Exception ex)
            {
                success = false;
                throw ex;
            }
            finally
            {
                w.Stop();
            }
        }
    }
}

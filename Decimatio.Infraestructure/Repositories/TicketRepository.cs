using BitMiracle.LibTiff.Classic;
using Decimatio.Domain.Entities;

namespace Decimatio.Infraestructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DataBaseConfig _connection;

        public TicketRepository(DataBaseConfig connection)
        {
            _connection = connection;
        }

        public async Task<long> AddTicket(Ticket ticket)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteScalarAsync<long>(Queries.INSERT_TICKET, ticket);
        }

        public async Task<int> AddTicketQR(TicketQR ticketQR)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            return await conn.ExecuteAsync(Queries.INSERT_TICKETQR, ticketQR);
        }

        public async Task<Ticket> GetInfoTicket(long idTicket)
        {
            var ticketDictionary = new Dictionary<long, Ticket>();
            using var conn = new SqlConnection(_connection.ConnectionString);
            var result = (await conn.QueryAsync<Ticket, Usuario, Evento, Sector, MedioPago, Lugar, Comuna, Ticket>(
                Queries.GET_INFO_TICKET,
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
                    ticketEntry.Evento.Lugar = lugar;
                    ticketEntry.Evento.Lugar.Comuna = comuna;
                    return ticketEntry;
                },
                    new { IdTicket = idTicket },
                    splitOn: "IdUsuario,IdEvento,IdSector,IdMedioPago,IdLugar,IdComuna"
                )).FirstOrDefault();
            if (result == null) throw new Exception("No se encuentra coindidencia para el Ticket");
            return result;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicket()
        {
            var ticketDictionary = new Dictionary<long, Ticket>();
            using var conn = new SqlConnection(_connection.ConnectionString);
            var result = (await conn.QueryAsync<Ticket, Usuario, Evento, Sector, MedioPago, Lugar, Comuna, Ticket>(
                Queries.GET_TICKETS,
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
                    ticketEntry.Evento.Lugar = lugar;
                    ticketEntry.Evento.Lugar.Comuna = comuna;
                    return ticketEntry;
                },
                    splitOn: "IdUsuario,IdEvento,IdSector,IdMedioPago,IdLugar,IdComuna"
                )).ToList();
            if (result == null) throw new Exception("No se encuentra coindidencia para el Ticket");
            return result;
        }

        public async Task<TicketQR> GetTicketQR(long idTicket)
        {
            var ticketDictionary = new Dictionary<long, TicketQR>();
            using var conn = new SqlConnection(_connection.ConnectionString);
            var ticket = (await conn.QueryAsync<TicketQR, Ticket, TicketQR>(
                  Queries.GET_TICKET_ID,
                  (ticketQR, ticket) =>
                  {
                      if (!ticketDictionary.TryGetValue(ticketQR.IdTicketQR, out var ticketEntry))
                      {
                          ticketEntry = ticketQR;
                          ticketDictionary.Add(ticketEntry.IdTicketQR, ticketEntry);

                      }

                      ticketEntry.Ticket = ticket;
                      return ticketEntry;
                  },
                  new { IdTicket = idTicket },
                  splitOn: "IdTicket"
              )).FirstOrDefault();
            return ticket ?? throw new Exception("No se encuentran tickets.");
        }

        public async Task<bool> DeleteDownTicket(long idTicket, bool activo)
        {
            using var conn = new SqlConnection(_connection.ConnectionString);
            var result = await conn.ExecuteAsync(Queries.DELETE_TICKET, new { IdTicket = idTicket, Activo = activo });
            return result > 0;
        }
    }
}

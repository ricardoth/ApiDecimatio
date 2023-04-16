
namespace Decimatio.Infraestructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private IDataBaseConnection _connection { get; }

        public TicketRepository(IDataBaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> AddTicket(Ticket ticket)
        {
            var result = await _connection.ExecuteAsync("INSERT_TICKET", Queries.INSERT_TICKET, ticket);
            return result.Value;
        }
    }
}

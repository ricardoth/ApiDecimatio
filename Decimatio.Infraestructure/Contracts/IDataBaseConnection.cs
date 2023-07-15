namespace Decimatio.Infraestructure.Contracts
{
    public interface IDataBaseConnection
    {
        Task<IEnumerable<T>> GetListAsync<T>(string queryName, string query);
        Task<IEnumerable<T>> GetListAsync<T>(string queryName, string query, object entity);
        Task<T> FirstOrDefaultAsync<T>(string queryName, string query, object entity);
        Task<IEnumerable<Ticket>> GetListTicketWithObjectAsync<T>(string queryName, string query, long? tickedId);
        Task<TicketQR> GetTicketQRAsync<T>(string queryName, string query, long? tickedId);
        Task<Evento> FirstOrDefaultEventoWithObjectAsync<T>(string queryName, string query, long eventoId);
        Task<int?> ExecuteAsync(string queryName, string query, object entity);
        Task<long?> ExecuteScalar(string queryName, string query, object entity);
        Task<T?> ExecuteScalar<T>(string queryName, string query, object entity);
        Task<T> QuerySingleAsync<T>(string queryName, string query, object entity);
        Task<T> ExecuteAsync<T>(string queryName, string query, object entity);
    }
}

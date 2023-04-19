namespace Decimatio.Infraestructure.Contracts
{
    public interface IDataBaseConnection
    {
        Task<IEnumerable<T>> GetListAsync<T>(string queryName, string query, object entity);
        Task<T> FirstOrDefaultAsync<T>(string queryName, string query, object entity);
        Task<Ticket> FirstOrDefaultWithObjectAsync<T>(string queryName, string query, long tickedId);
        Task<int?> ExecuteAsync(string queryName, string query, object entity);
        Task<long?> ExecuteScalar(string queryName, string query, object entity);
    }
}

namespace Decimatio.Infraestructure.Contracts
{
    public interface IDataBaseConnection
    {
        Task<IEnumerable<T>> GetListAsync<T>(string queryName, string query);
        Task<T> FirstOrDefaultAsync<T>(string queryName, string query, object entity);
        Task<int?> ExecuteAsync(string queryName, string query, object entity);
    }
}

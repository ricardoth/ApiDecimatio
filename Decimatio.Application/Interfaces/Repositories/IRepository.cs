namespace Decimatio.Application.Interfaces.Repositories
{
    public interface IRepository
    {
        Task<int> GetTotalCount(string tableName);
    }
}

namespace Decimatio.Domain.Interfaces
{
    public interface IRepository
    {
        Task<int> GetTotalCount(string tableName);
    }
}

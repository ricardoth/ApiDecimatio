namespace Decimatio.Application.Interfaces.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllRegions();
    }
}

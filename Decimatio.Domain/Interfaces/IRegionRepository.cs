namespace Decimatio.Domain.Interfaces
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllRegions();
    }
}

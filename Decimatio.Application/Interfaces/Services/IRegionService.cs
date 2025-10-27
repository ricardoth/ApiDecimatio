namespace Decimatio.Domain.Interfaces
{
    public interface IRegionService
    {
        Task<IEnumerable<Region>> GetAllRegions();
    }
}

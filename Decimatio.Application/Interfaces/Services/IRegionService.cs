namespace Decimatio.Application.Interfaces.Services
{
    public interface IRegionService
    {
        Task<IEnumerable<RegionDto>> GetAllRegions();
    }
}

﻿namespace Decimatio.Infraestructure.Services
{
    internal sealed class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;

        public RegionService(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;        
        }

        public async Task<IEnumerable<Domain.Entities.Region>> GetAllRegions()
        {
            try
            {
                return await _regionRepository.GetAllRegions();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

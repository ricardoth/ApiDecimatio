namespace Decimatio.Application.Services
{
    internal sealed class SectorService : ISectorService
    {
		private readonly ISectorRepository _sectorRepository;
        private readonly IValidator<CreateSectorDto> _createSectorValidator;
        private readonly IValidator<UpdateSectorDto> _updateSectorValidator;
        private readonly IMapper _mapper;

        public SectorService(ISectorRepository sectorRepository,
            IValidator<CreateSectorDto> createSectorValidator,
            IValidator<UpdateSectorDto> updateSectorValidator,
            IMapper mapper)
        {
            _sectorRepository = sectorRepository;
            _createSectorValidator = createSectorValidator;
            _updateSectorValidator = updateSectorValidator;
            _mapper = mapper;

        }

        public async Task<IEnumerable<SectorDto>> GetAllSectores()
        {
            var result = await _sectorRepository.GetAllSectores();
            if (!result.Any())
                throw new NotFoundException("No se encontraron sectores"); 

            var sectores = _mapper.Map<IEnumerable<SectorDto>>(result);
            return sectores;
        }

        public async Task<SectorDto> GetById(long idSector)
        {
            var result = await _sectorRepository.GetById(idSector);
            if (result is null)
                throw new NotFoundException($"No se encontró el sector indicado");

            var sector = _mapper.Map<SectorDto>(result);
            return sector;  
        }

        public async Task<IEnumerable<SectorDto>> GetSectoresByEvento(int idEvento)
        {
            var result = await _sectorRepository.GetSectoresByEvento(idEvento);
            if (!result.Any())
                throw new NotFoundException($"No se encontraron sectores del evento solicitado");

            var sectores = _mapper.Map<IEnumerable<SectorDto>>(result);
            return sectores;
        }

        public async Task AddSector(CreateSectorDto createSectorDto)
        {
            var validationResult = _createSectorValidator.Validate(createSectorDto);
            if (!validationResult.IsValid)
            {
                var errores = validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList();
                throw new ValidationResultException(errores);
            }

            var sector = _mapper.Map<Sector>(createSectorDto);
            await _sectorRepository.AddSector(sector);
        }

        public async Task<bool> UpdateSector(UpdateSectorDto updateSectorDto)
        {
            if (updateSectorDto.IdSector <= 0)
                throw new BadRequestException("El sector es inválido");

            var validationResult = _updateSectorValidator.Validate(updateSectorDto);
            if (!validationResult.IsValid)
            {
                var errores = validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList();
                throw new ValidationResultException(errores);
            }

            var sectorBd = await _sectorRepository.GetById((long)updateSectorDto.IdSector);
            var sector = _mapper.Map(updateSectorDto, sectorBd);
            var result = await _sectorRepository.UpdateSector(sector);
            return result;
        }

        public async Task<bool> DeleteSector(int idSector)
        {
            if (idSector <= 0)
                throw new NotFoundException("El sector es inválido");

            var sector = await GetById(idSector);
            return await _sectorRepository.DeleteSector(sector.IdSector);
        }
    }
}

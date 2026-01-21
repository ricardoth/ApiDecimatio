using Decimatio.Common.Interfaces;
using Decimatio.Domain.CustomEntities;
using Decimatio.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace Decimatio.Application.Services
{
    internal sealed class EventoService : IEventoService
    {
		private readonly IEventoRepository _eventoRepository;
        private readonly IBlobFilesService _blobFilesService;
        private readonly BlobContainerConfig _containerConfig;
        private readonly PaginationOptions _paginationOptions;
        private readonly IValidator<CreateEventoDto> _createEventoValidator;
        private readonly IValidator<UpdateEventoDto> _updateEventoValidator;
        private readonly IMapper _mapper;

        public EventoService(IEventoRepository eventoRepository,
            IBlobFilesService blobFilesService,
            BlobContainerConfig containerConfig,
            IOptions<PaginationOptions> paginationOptions,
            IValidator<CreateEventoDto> createEventoValidator,
            IValidator<UpdateEventoDto> updateEventoValidator,
            IMapper mapper)
        {
            _eventoRepository = eventoRepository;
            _blobFilesService = blobFilesService;
            _containerConfig = containerConfig;
            _paginationOptions = paginationOptions.Value;
            _createEventoValidator = createEventoValidator;
            _updateEventoValidator = updateEventoValidator; 
            _mapper = mapper;
        }

        public async Task<IEnumerable<Evento>> GetAllEventos()
        {
		
			var result = await _eventoRepository.GetAllEventos();
            if (result == null)
                throw new NotFoundException("No se encuentran eventos en nuestros registros");
            
            var tasks = result.Select(async evento =>
            {
                string imageNamePath = _containerConfig.FolderFlyerName + evento.Flyer;
                evento.UrlImagenFlyer = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
                return evento;
            });

            return await Task.WhenAll(tasks);
        }
        public async Task<Evento> GetById(int idEvento)
        {
			var result = await _eventoRepository.GetById(idEvento);
			if (result == null)
				throw new NotFoundException("No existe el evento solicitado");

			string imageNamePath = _containerConfig.FolderFlyerName + result.Flyer;
            result.UrlImagenFlyer = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
            return result;
        }

        public async Task AddEvento(CreateEventoDto createEventoDto)
        {
            var validations = _createEventoValidator.Validate(createEventoDto);
            if (!validations.IsValid)
            {
                var errores = validations.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList();
                throw new ValidationResultException(errores);
            }

            if (!string.IsNullOrEmpty(createEventoDto.Base64ImagenFlyer)) 
            {
                string imageNamePath = _containerConfig.FolderFlyerName + createEventoDto.Flyer;
                var flyerContent = Convert.FromBase64String(createEventoDto.Base64ImagenFlyer);
                await _blobFilesService.AddFlyerBlobStorage(flyerContent, imageNamePath);
            }

            var evento = _mapper.Map<Evento>(createEventoDto);
            await _eventoRepository.AddEvento(evento);
        }

        public async Task<bool> UpdateEvento(UpdateEventoDto updateEventoDto)
        {
            var validations = _updateEventoValidator.Validate(updateEventoDto);
            if (!validations.IsValid)
            {
                var errores = validations.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList();
                throw new ValidationResultException(errores);
            }

            if (!string.IsNullOrEmpty(updateEventoDto.Base64ImagenFlyer))
            {
                string imageNamePath = _containerConfig.FolderFlyerName + updateEventoDto.Flyer;
                var flyerContent = Convert.FromBase64String(updateEventoDto.Base64ImagenFlyer);
                await _blobFilesService.AddFlyerBlobStorage(flyerContent, imageNamePath);
            }

            var eventoBd = await _eventoRepository.GetById((long)updateEventoDto.IdEvento);
            var evento = _mapper.Map(updateEventoDto, eventoBd);
            return await _eventoRepository.UpdateEvento(evento);
        }

        public async Task<bool> DeleteEvento(int idEvento)
        {
            return await _eventoRepository.DeleteEvento(idEvento);
        }

        public async Task<IEnumerable<Evento>> GetEventosFilter(string filtro)
        {
            var result = await _eventoRepository.GetEventosFilter(filtro);
            var tasks = result.Select(async evento =>
            {
                string imageNamePath = _containerConfig.FolderFlyerName + evento.Flyer;
                evento.UrlImagenFlyer = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
                return evento;
            });

            if (result is null)
                throw new NotFoundException("No se pudo encontrar los eventos indicados");

            return await Task.WhenAll(tasks);
        }

        public async Task<PagedList<Evento>> GetAllEventosPaginated(EventoQueryFilter filtros)
        {
            filtros.PageNumber = filtros.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filtros.PageNumber;
            filtros.PageSize = filtros.PageSize == 0 ? _paginationOptions.DefaultPageSize : filtros.PageSize;

            var eventos = await _eventoRepository.GetAllEventos();

            if (eventos is null)
                throw new NotFoundException("No se encontraron eventos");

            if (filtros.IdEvento > 0)
                eventos = eventos.Where(x => x.IdEvento == filtros.IdEvento);

            eventos = eventos.Where(x => x.Activo == true);

            var pagedList = PagedList<Evento>.Create(eventos, filtros.PageNumber, filtros.PageSize);
            foreach (var evento in pagedList)
            {
                string imageNamePath = _containerConfig.FolderFlyerName + evento.Flyer;
                evento.UrlImagenFlyer = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
            }
            return pagedList;
        }

        public async Task<IEnumerable<Evento>> GetAllEventosCombobox()
        {
            var result = await _eventoRepository.GetAllEventos();
            if (result == null)
                throw new NotFoundException("No se encuentran eventos en nuestros registros");

            return result;
        }
    }
}

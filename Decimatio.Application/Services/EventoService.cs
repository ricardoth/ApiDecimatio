using Decimatio.Common.Interfaces;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<EventoService> _logger;

        public EventoService(IEventoRepository eventoRepository,
            IBlobFilesService blobFilesService,
            BlobContainerConfig containerConfig,
            IOptions<PaginationOptions> paginationOptions,
            IValidator<CreateEventoDto> createEventoValidator,
            IValidator<UpdateEventoDto> updateEventoValidator,
            IMapper mapper,
            ILogger<EventoService> logger)
        {
            _eventoRepository = eventoRepository;
            _blobFilesService = blobFilesService;
            _containerConfig = containerConfig;
            _paginationOptions = paginationOptions.Value;
            _createEventoValidator = createEventoValidator;
            _updateEventoValidator = updateEventoValidator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<EventoDto>> GetAllEventos()
        {
			var eventos = await _eventoRepository.GetAllEventos();
            if (eventos == null)
                throw new NotFoundException("No se encuentran eventos en nuestros registros");

            await LoadEventImage(eventos);
            return _mapper.Map<IEnumerable<EventoDto>>(eventos);
        }

        public async Task<EventoDto> GetById(int idEvento)
        {
            if(idEvento <= 0)
                throw new NotFoundException("Debe ingresar un evento válido");

            var result = await _eventoRepository.GetById(idEvento);
			if (result == null)
				throw new NotFoundException("No existe el evento solicitado");

			string imageNamePath = _containerConfig.FolderFlyerName + result.Flyer;
            result.UrlImagenFlyer = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);

            var mappedResult = _mapper.Map<EventoDto>(result);
            return mappedResult;
        }

        public async Task AddEvento(CreateEventoDto createEventoDto)
        {
            var validations = _createEventoValidator.Validate(createEventoDto);
            if (!validations.IsValid)
            {
                var errores = validations.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList();
                throw new ValidationResultException(errores);
            }

            await SaveFlyerToBlobStorage(createEventoDto.Flyer, createEventoDto.Base64ImagenFlyer);

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

            var eventoBd = await _eventoRepository.GetById((long)updateEventoDto.IdEvento);
            if (eventoBd is null)
                throw new NotFoundException("No existe el evento solicitado");

            if (string.IsNullOrEmpty(updateEventoDto.Flyer) && !string.IsNullOrEmpty(updateEventoDto.Base64ImagenFlyer))
                updateEventoDto.Flyer = eventoBd.Flyer;

            await SaveFlyerToBlobStorage(updateEventoDto.Flyer, updateEventoDto.Base64ImagenFlyer);

            var evento = _mapper.Map(updateEventoDto, eventoBd);
            return await _eventoRepository.UpdateEvento(evento);
        }

        public async Task<bool> DeleteEvento(int idEvento)
        {
            return await _eventoRepository.DeleteEvento(idEvento);
        }

        public async Task<IEnumerable<EventoDto>> GetEventosFilter(string filtro)
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

            var eventos = await Task.WhenAll(tasks);
            var eventosDtos = _mapper.Map<IEnumerable<EventoDto>>(eventos);
            return eventosDtos;
        }

        public async Task<(IEnumerable<EventoDto>, MetaData)> GetAllEventosPaginated(EventoQueryFilter filtros)
        {
            NormalizePagination(filtros);

            var eventos = await _eventoRepository.GetAllEventosPaginated(filtros);
            var counter = await _eventoRepository.GetCounterEvento();

            if (eventos is null)
                throw new NotFoundException("No se encontraron eventos");

            eventos = eventos.Where(x => x.Activo == true);

            var pagedList = PagedList<Evento>.CreatePaginationFromDb(eventos, counter, filtros.PageNumber, filtros.PageSize);

            await LoadEventImage(pagedList);

            var metaData = PagedListExtensions.ToMetaData(pagedList);
            var eventosDtos = _mapper.Map<IEnumerable<EventoDto>>(pagedList);
            return (eventosDtos, metaData);
        }

        public async Task<IEnumerable<EventoDto>> GetAllEventosCombobox()
        {
            var result = await _eventoRepository.GetAllEventos();
            if (result == null)
                throw new NotFoundException("No se encuentran eventos en nuestros registros");

            var mappedResult = _mapper.Map<IEnumerable<EventoDto>>(result);
            return mappedResult;
        }

        private void NormalizePagination(EventoQueryFilter filtros)
        {
            filtros.PageNumber = filtros.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filtros.PageNumber;
            filtros.PageSize = filtros.PageSize == 0 ? _paginationOptions.DefaultPageSize : filtros.PageSize;
        }

        private async Task SaveFlyerToBlobStorage(string flyer, string base64ImagenFlyer)
        {
            if (string.IsNullOrEmpty(base64ImagenFlyer))
            {
                _logger.LogWarning("AddEvento/UpdateEvento: no se recibió Base64ImagenFlyer, el evento se guardará sin actualizar el flyer.");
                return;
            }

            if (string.IsNullOrEmpty(flyer))
            {
                throw new ValidationResultException(new List<string> { "Flyer: debe indicar el nombre del archivo cuando se envía Base64ImagenFlyer." });
            }

            string imageNamePath = _containerConfig.FolderFlyerName + flyer;
            byte[] flyerContent;
            try
            {
                // Algunos clientes envían el Data URI completo (ej: "data:image/png;base64,xxxx").
                var base64Data = base64ImagenFlyer.Contains(",") ? base64ImagenFlyer.Substring(base64ImagenFlyer.IndexOf(",") + 1) : base64ImagenFlyer;
                flyerContent = Convert.FromBase64String(base64Data);
            }
            catch (FormatException ex)
            {
                _logger.LogWarning(ex, "AddEvento/UpdateEvento: Base64ImagenFlyer inválido para {ImageNamePath}.", imageNamePath);
                throw new ValidationResultException(new List<string> { "Base64ImagenFlyer: el contenido no es un Base64 válido." });
            }

            await _blobFilesService.AddFlyerBlobStorage(flyerContent, imageNamePath);
            _logger.LogInformation("Flyer guardado correctamente en Blob Storage: {ImageNamePath}.", imageNamePath);
        }

        private async Task LoadEventImage(IEnumerable<Evento> eventos)
        { 
            var tasks = eventos.Select(async evento =>
            {
                string imageNamePath = _containerConfig.FolderFlyerName + evento.Flyer;
                evento.UrlImagenFlyer = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
                return evento;
            });

            await Task.WhenAll(tasks);
        }
    }
}

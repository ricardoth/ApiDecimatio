namespace Decimatio.Infraestructure.Services
{
    internal sealed class EventoService : IEventoService
    {
		private readonly IEventoRepository _eventoRepository;
        private readonly IBlobFilesService _blobFilesService;
        private readonly BlobContainerConfig _containerConfig;
        private readonly PaginationOptions _paginationOptions;

        public EventoService(IEventoRepository eventoRepository, 
            IBlobFilesService blobFilesService, 
            BlobContainerConfig containerConfig,
            IOptions<PaginationOptions> paginationOptions)
        {
            _eventoRepository = eventoRepository;
            _blobFilesService = blobFilesService;
            _containerConfig = containerConfig;
            _paginationOptions = paginationOptions.Value;
        }

        public async Task<IEnumerable<Evento>> GetAllEventos()
        {
		
			var result = await _eventoRepository.GetAllEventos();
            if (result == null)
                throw new BadRequestException("No se encuentran eventos en nuestros registros");
            
            var tasks = result.Select(async evento =>
            {
                string imageNamePath = _containerConfig.FolderFlyerName + evento.Flyer;
                evento.ContenidoFlyer = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
                return evento;
            });

            return await Task.WhenAll(tasks);
        }


        public async Task<Evento> GetById(int idEvento)
        {
			try
			{
				var result = await _eventoRepository.GetById(idEvento);
				if (result == null)
					throw new Exception("Ha ocurrido un error al obtener el evento desde el Repositorio");

				string imageNamePath = _containerConfig.FolderFlyerName + result.Flyer;
                result.ContenidoFlyer = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
                return result;
			}
			catch (Exception ex)
			{
				throw new Exception($"Ha ocurrido un error en EventoService: {ex.Message}", ex);
			}
        }

        public async Task AddEvento(Evento evento)
        {
            try
            {
                if (evento.Flyer != null || evento.Flyer != "") 
                {
                    string imageNamePath = _containerConfig.FolderFlyerName + evento.Flyer;
                    var flyerContent = Convert.FromBase64String(evento.ContenidoFlyer);
                    await _blobFilesService.AddFlyerBlobStorage(flyerContent, imageNamePath);
                }
                evento.ContenidoFlyer = "";
                await _eventoRepository.AddEvento(evento);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un error al agregar el evento: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateEvento(Evento evento)
        {
            try
            {
                if (evento.Flyer != null || evento.Flyer != "")
                {
                    string imageNamePath = _containerConfig.FolderFlyerName + evento.Flyer;
                    var flyerContent = Convert.FromBase64String(evento.ContenidoFlyer);
                    await _blobFilesService.AddFlyerBlobStorage(flyerContent, imageNamePath);
                }
                evento.ContenidoFlyer = "";
                return await _eventoRepository.UpdateEvento(evento);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un error al editar el evento: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteEvento(int idEvento)
        {
            try
            {
                return await _eventoRepository.DeleteEvento(idEvento);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un error al eliminar el evento: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Evento>> GetEventosFilter(string filtro)
        {
            try
            {
                var result = await _eventoRepository.GetEventosFilter(filtro);
                var tasks = result.Select(async evento =>
                {
                    string imageNamePath = _containerConfig.FolderFlyerName + evento.Flyer;
                    evento.ContenidoFlyer = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
                    return evento;
                });

                if (result is null)
                    throw new BadRequestException("No se pudo encontrar eventos indicados");

                return await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un error al obtener los evento: {ex.Message}", ex);
            }
        }

        public async Task<PagedList<Evento>> GetAllEventosPaginated(EventoQueryFilter filtros)
        {
            filtros.PageNumber = filtros.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filtros.PageNumber;
            filtros.PageSize = filtros.PageSize == 0 ? _paginationOptions.DefaultPageSize : filtros.PageSize;

            var eventos = await _eventoRepository.GetAllEventos();

            if (eventos is null)
                throw new BadRequestException("No se encontraron eventos");

            if (filtros.IdEvento > 0)
                eventos = eventos.Where(x => x.IdEvento == filtros.IdEvento);

            eventos = eventos.Where(x => x.Activo == true);

            var pagedList = PagedList<Evento>.Create(eventos, filtros.PageNumber, filtros.PageSize);
            foreach (var evento in pagedList)
            {
                string imageNamePath = _containerConfig.FolderFlyerName + evento.Flyer;
                evento.ContenidoFlyer = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
            }
            return pagedList;
        }
    }
}

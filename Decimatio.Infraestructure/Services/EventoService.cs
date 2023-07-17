namespace Decimatio.Infraestructure.Services
{
    public class EventoService : IEventoService
    {
		private readonly IEventoRepository _eventoRepository;
        private readonly IBlobFilesService _blobFilesService;
        private readonly BlobContainerConfig _containerConfig;

        public EventoService(IEventoRepository eventoRepository, IBlobFilesService blobFilesService, BlobContainerConfig containerConfig)
        {
            _eventoRepository = eventoRepository;
            _blobFilesService = blobFilesService;
            _containerConfig = containerConfig;
        }

        public async Task<IEnumerable<Evento>> GetAllEventos()
        {
			try
			{
				var result = await _eventoRepository.GetAllEventos();
                return result;
			}
			catch (Exception ex)
			{
				throw new Exception($"Ha ocurrido un error en EventoService: {ex.Message}", ex);
			}
        }

        public async Task<Evento> GetById(int idEvento)
        {
			try
			{
				var result = await _eventoRepository.GetById(idEvento);
				if (result == null)
					throw new Exception("Ha ocurrido un error al obtener el evento desde el Repositorio");

				var flyer = await _blobFilesService.GetImageFromBlobStorage(result.Flyer, _containerConfig.FolderFlyerName);
				result.Flyer = flyer;	

				return result;
				
			}
			catch (Exception ex)
			{
				throw new Exception($"Ha ocurrido un error en EventoService: {ex.Message}", ex);
			}
        }
    }
}

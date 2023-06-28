namespace Decimatio.Infraestructure.Services
{
    public class EventoService : IEventoService
    {
		private readonly IEventoRepository _eventoRepository;
        private readonly IBlobFilesService _blobFilesService;

        public EventoService(IEventoRepository eventoRepository, IBlobFilesService blobFilesService)
        {
            _eventoRepository = eventoRepository;
            _blobFilesService = blobFilesService;

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

				throw;
			}
        }

        public async Task<Evento> GetById(int idEvento)
        {
			try
			{
				var result = await _eventoRepository.GetById(idEvento);
				if (result == null)
					throw new Exception("Ha ocurrido un error al obtener el evento desde el Repositorio");

				var flyer = await _blobFilesService.GetImageFromBlobStorage(result.Flyer);
				result.Flyer = flyer;	

				return result;
				
			}
			catch (Exception ex)
			{

				throw;
			}
        }
    }
}

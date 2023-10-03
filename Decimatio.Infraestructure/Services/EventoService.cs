namespace Decimatio.Infraestructure.Services
{
    public class EventoService : IEventoService
    {
		private readonly IEventoRepository _eventoRepository;
        private readonly IBlobFilesService _blobFilesService;
        private readonly BlobContainerConfig _containerConfig;

        public EventoService(IEventoRepository eventoRepository, 
            IBlobFilesService blobFilesService, 
            BlobContainerConfig containerConfig)
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

				string imageNamePath = _containerConfig.FolderFlyerName + result.Flyer;

                var flyer = await _blobFilesService.GetImageFromBlobStorage(imageNamePath);
				result.Flyer = flyer;	
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
                    string imageNamePath = _containerConfig.FolderFlyerName + evento.NombreEvento + ".jpg";
                    var flyer = Convert.FromBase64String(evento.Flyer);
                    await _blobFilesService.AddFlyerBlobStorage(flyer, imageNamePath);
                    evento.Flyer = evento.NombreEvento + ".jpg";
                }
                await _eventoRepository.AddEvento(evento);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha ocurrido un error al agregar el evento: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateEvento(Evento evento)
        {
            throw new NotImplementedException();
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
    }
}

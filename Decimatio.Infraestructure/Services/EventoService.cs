namespace Decimatio.Infraestructure.Services
{
    public class EventoService : IEventoService
    {
		private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
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
    }
}

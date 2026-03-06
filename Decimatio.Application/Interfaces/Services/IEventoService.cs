namespace Decimatio.Application.Interfaces.Services
{
    public interface IEventoService
    {
        Task<IEnumerable<EventoDto>> GetAllEventos();
        Task<IEnumerable<EventoDto>> GetAllEventosCombobox();
        Task<(IEnumerable<EventoDto>, MetaData)> GetAllEventosPaginated(EventoQueryFilter filtros);
        Task<EventoDto> GetById(int idEvento);
        Task AddEvento(CreateEventoDto createEventoDto);
        Task<bool> UpdateEvento(UpdateEventoDto updateEventoDto);
        Task<bool> DeleteEvento(int idEvento);
        Task<IEnumerable<EventoDto>> GetEventosFilter(string filtro);
    }
}

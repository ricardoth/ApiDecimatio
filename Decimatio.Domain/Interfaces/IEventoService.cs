using Decimatio.Domain.DTOs;

namespace Decimatio.Domain.Interfaces
{
    public interface IEventoService
    {
        Task<IEnumerable<Evento>> GetAllEventos();
        Task<IEnumerable<Evento>> GetAllEventosCombobox();
        Task<PagedList<Evento>> GetAllEventosPaginated(EventoQueryFilter filtros);
        Task<Evento> GetById(int idEvento);
        Task AddEvento(CreateEventoDto createEventoDto);
        Task<bool> UpdateEvento(UpdateEventoDto updateEventoDto);
        Task<bool> DeleteEvento(int idEvento);
        Task<IEnumerable<Evento>> GetEventosFilter(string filtro);
    }
}

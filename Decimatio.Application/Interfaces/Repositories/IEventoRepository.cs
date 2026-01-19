namespace Decimatio.Application.Interfaces.Repositories
{
    public interface IEventoRepository
    {
        Task<IEnumerable<Evento>> GetAllEventos();
        Task<Evento> GetById(long idEvento);
        Task<int> AddEvento(Evento evento);
        Task<bool> UpdateEvento(Evento evento);
        Task<bool> DeleteEvento(int idEvento);
        Task<IEnumerable<Evento>> GetEventosFilter(string filtro);
    }
}

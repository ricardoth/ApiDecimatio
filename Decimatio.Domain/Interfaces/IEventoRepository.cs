namespace Decimatio.Domain.Interfaces
{
    public interface IEventoRepository
    {
        Task<IEnumerable<Evento>> GetAllEventos();
        Task<Evento> GetById(int idEvento);
        Task<int> AddEvento(Evento evento);
        Task<bool> UpdateEvento(Evento evento);
        Task<bool> DeleteEvento(int idEvento);
    }
}

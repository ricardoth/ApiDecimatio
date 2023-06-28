namespace Decimatio.Domain.Interfaces
{
    public interface IEventoService
    {
        Task<IEnumerable<Evento>> GetAllEventos();
        Task<Evento> GetById(int idEvento);
    }
}

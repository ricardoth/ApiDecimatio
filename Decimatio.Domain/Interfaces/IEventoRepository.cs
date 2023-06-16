namespace Decimatio.Domain.Interfaces
{
    public interface IEventoRepository
    {
        Task<IEnumerable<Evento>> GetAllEventos();
    }
}

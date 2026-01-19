namespace Decimatio.Application.Interfaces.Repositories
{
    public interface IPreferenceRepository
    {
        Task<IEnumerable<PreferenceTicket>> GetAll();
        Task<IEnumerable<PreferenceTicket>> GetByPreferenceCode(string preferenceCode);
    }
}
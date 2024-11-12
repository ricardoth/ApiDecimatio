namespace Decimatio.Domain.Interfaces
{
    public interface IPreferenceRepository
    {
        Task<IEnumerable<PreferenceTicket>> GetAll();
        Task<IEnumerable<PreferenceTicket>> GetByPreferenceCode(string preferenceCode);
    }
}
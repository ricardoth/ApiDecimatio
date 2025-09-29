using Decimatio.Domain.DTOs;

namespace Decimatio.Domain.Interfaces
{
    public interface ILugarService
    {
        Task<IEnumerable<LugarDto>> GetAllLugares();
        Task<LugarDto> GetById(int idLugar);
        Task AddLugar(CreateLugarDto createLugarDto);
        Task<bool> UpdateLugar(UpdateLugarDto updateLugarDto);
        Task<bool> DeleteLugar(int idLugar);
    }
}

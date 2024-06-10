using DOTNET_Angular_DUC_SHOP3a.Models.DTO;

namespace DOTNET_Angular_DUC_SHOP3a.Repository.Interface
{
    public interface IDistrictRepository : IGenericRepository<District>
    {
        Task<bool> AddUpdate(DistrictAddUpdateDTO modelDTO);
        Task<IEnumerable<District>> GetAllWithDetails();
    }
}

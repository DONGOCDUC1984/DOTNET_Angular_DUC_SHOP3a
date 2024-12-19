using DOTNET_Angular_DUC_SHOP3a.Models.DTO;

namespace DOTNET_Angular_DUC_SHOP3a.Repository.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> AddUpdate(ProductAddUpdateDTO modelDTO);
        Task<List<Product>> GetAllWithDetails(string searchStr = "",
                   int categoryId = 0, int provinceCityId = 0, int districtId = 0,
                   double minPrice = 0.0, double maxPrice = 0.0);
        Task<Product> GetByIdWithDetails(int id);
        Task<Product> GetOnlyProductById(int id);
    }
}

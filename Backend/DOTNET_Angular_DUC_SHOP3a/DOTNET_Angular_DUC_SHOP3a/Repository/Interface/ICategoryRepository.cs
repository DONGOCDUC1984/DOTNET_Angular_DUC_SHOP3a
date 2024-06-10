namespace DOTNET_Angular_DUC_SHOP3a.Repository.Interface
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<bool> AddUpdate(Category category);
    }
}

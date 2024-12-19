namespace DOTNET_Angular_DUC_SHOP3a.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> Delete(int id);
    }
}

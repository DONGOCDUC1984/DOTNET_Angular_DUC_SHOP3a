namespace DOTNET_Angular_DUC_SHOP3a.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICartRepository cartRepository { get; }
        ICategoryRepository categoryRepository { get; }
        IDistrictRepository districtRepository { get; }
        IOrderRepository orderRepository { get; }
        IProductRepository productRepository { get; }
        IProvinceCityRepository provinceCityRepository { get; }
        Task Save();
    }
}

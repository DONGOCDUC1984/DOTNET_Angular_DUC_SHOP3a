namespace DOTNET_Angular_DUC_SHOP3a.Repository.Interface
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart> GetCartByUserId(string UserId);
        Task<int> GetCartLen(string UserId);
        Task<double> GetTotalCost(string UserId);
        Task<bool> AddCartItem(string UserId, int ProductId);
        Task<bool> DecreaseCartItem(string UserId, int ProductId);
        Task<bool> RemoveCartItem(string UserId, int ProductId);

    }
}

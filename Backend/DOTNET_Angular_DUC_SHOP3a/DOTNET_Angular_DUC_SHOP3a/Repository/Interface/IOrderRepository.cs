namespace DOTNET_Angular_DUC_SHOP3a.Repository.Interface
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<bool> Add(string UserId,string UserTel, string UserAddress, double totalCost);
        Task<IEnumerable<Order>> GetAllWithDetails();
        Task<IEnumerable<Order>> GetOrdersByUserId(string UserId);

    }
}

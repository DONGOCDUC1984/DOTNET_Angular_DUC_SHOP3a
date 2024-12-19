
namespace DOTNET_Angular_DUC_SHOP3a.Repository.Implementation
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> Add(string UserId,
            string UserTel, string UserAddress, double totalCost)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var cart = await _context.Carts
                          .Include(x => x.CartItems)
                          .ThenInclude(x => x.Product)
                          .Where(x => x.ApplicationUser.Id == UserId)
                          .FirstOrDefaultAsync();
                if (cart == null)
                {
                    throw new Exception("Cart does not exist");
                }
                if (cart.CartItems.Count == 0)
                {
                    throw new Exception("Cart is empty");
                }
                var newOrder = new Order()
                {
                    ApplicationUser = await _context.ApplicationUsers
                                 .FindAsync(UserId),
                    UserTel = UserTel,
                    UserAddress = UserAddress,
                    totalCost = totalCost,
                };
                await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();

                foreach (var item in cart.CartItems)
                {
                    var newOrderItem = new OrderItem()
                    {
                        Order = newOrder,
                        Product = item.Product,
                        Quantity = item.Quantity,
                    };
                    await _context.OrderItems.AddAsync(newOrderItem);
                    await _context.SaveChangesAsync();
                }
                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();

                transaction.Commit();
                return true;

            }
            catch (Exception)
            {
                return false; ;
            }
        }

        public async Task<IEnumerable<Order>> GetAllWithDetails()
        {
            var data = await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .ToListAsync();
            return data;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(string UserId)
        {
            var data = await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Where(x => x.ApplicationUser.Id == UserId)
                .ToListAsync();
            return data;
        }


    }

}

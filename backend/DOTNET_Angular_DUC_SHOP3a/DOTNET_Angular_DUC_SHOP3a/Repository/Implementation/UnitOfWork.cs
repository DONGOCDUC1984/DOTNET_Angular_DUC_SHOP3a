using DOTNET_Angular_DUC_SHOP3a.Redis;
using DOTNET_Angular_DUC_SHOP3a.Repository.Interface;

namespace DOTNET_Angular_DUC_SHOP3a.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IRedisCacheService _redisCacheService;
        public ICartRepository cartRepository { get; private set; }
        public ICategoryRepository categoryRepository { get; private set; }
        public IDistrictRepository districtRepository { get; private set; }
        public IOrderRepository orderRepository { get; private set; }
        public IProductRepository productRepository { get; private set; }
        public IProvinceCityRepository provinceCityRepository { get; private set; }
        public UnitOfWork(AppDbContext context, IRedisCacheService redisCacheService)
        {
            _context = context;
            _redisCacheService = redisCacheService;
            cartRepository = new CartRepository(_context);
            categoryRepository = new CategoryRepository(_context);
            districtRepository = new DistrictRepository(_context);
            orderRepository = new OrderRepository(_context);
            productRepository = new ProductRepository(_context,_redisCacheService);
            provinceCityRepository = new ProvinceCityRepository(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}

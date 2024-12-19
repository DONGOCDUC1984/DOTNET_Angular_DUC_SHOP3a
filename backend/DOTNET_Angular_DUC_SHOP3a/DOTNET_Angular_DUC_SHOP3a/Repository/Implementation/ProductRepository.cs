
using DOTNET_Angular_DUC_SHOP3a.Redis;

namespace DOTNET_Angular_DUC_SHOP3a.Repository.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IRedisCacheService _redisCacheService;
        public ProductRepository(AppDbContext context, IRedisCacheService redisCacheService) : base(context)
        {
            _context = context;
            _redisCacheService = redisCacheService;
        }
        public async Task<bool> AddUpdate(ProductAddUpdateDTO modelDTO)
        {
            try
            {
                var category = await _context.Categories.FindAsync(modelDTO.CategoryId);
                var district = await _context.Districts.FindAsync(modelDTO.DistrictId);
                // Add
                if (modelDTO.Id == 0)
                {
                    // Find a new Id .Without newId, there will be an error.
                    var listId = (from x in _context.Products
                                  select x.Id).ToList();
                    int newId = listId.Max() + 1;
                    // Create a new product
                    var product = new Product()
                    {
                        Id = newId,
                        Name = modelDTO.Name,
                        Description = modelDTO.Description,
                        Price = modelDTO.Price,
                        Category = category,
                        District = district,
                        ImageUrl = modelDTO.ImageUrl
                    };
                    await _context.Products.AddAsync(product);
                }
                // Update
                else
                {
                    var product = new Product()
                    {
                        Id = modelDTO.Id,
                        Name = modelDTO.Name,
                        Description = modelDTO.Description,
                        Price = modelDTO.Price,
                        Category = category,
                        District = district,
                        ImageUrl = modelDTO.ImageUrl
                    };
                    _context.Products.Update(product);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // Without "ref" in the following line,filteredData will not change
        private void Search(ref List<Product> filteredData, string searchStr = "", int categoryId = 0,
            int provinceCityId = 0, int districtId = 0, double minPrice = 0.0, double maxPrice = 0.0)
        {
            searchStr = searchStr?.ToLower();
            if (!string.IsNullOrEmpty(searchStr))
            {
                filteredData = filteredData.Where(x => x.Name.ToLower().Contains(searchStr)
                                               || x.Description.ToLower().Contains(searchStr)

                                               ).ToList();
            }
            if (categoryId > 0)
            {
                filteredData = filteredData.Where(x => x.Category.Id  == categoryId).ToList();
            }
            if (provinceCityId > 0)
            {
                filteredData = filteredData.Where(x => x.District.ProvinceCity.Id == provinceCityId).ToList();
            }
            if (districtId > 0)
            {
                filteredData = filteredData.Where(x => x.District.Id == districtId).ToList();
            }
            if (minPrice != 0)
            {
                filteredData = filteredData.Where(x => x.Price >= minPrice).ToList();
            }
            if (maxPrice != 0)
            {
                filteredData = filteredData.Where(x => x.Price <= maxPrice).ToList();
            }
        }
        public async Task<List<Product>> GetAllWithDetails(string searchStr = "",
                   int categoryId = 0, int provinceCityId = 0, int districtId = 0,
                    double minPrice = 0.0, double maxPrice = 0.0)
        {
            searchStr = searchStr?.ToLower();
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            var cacheData = _redisCacheService.GetData<List<Product>>("Product");
            var filteredData = cacheData;
            if (cacheData != null)
            {
                Search(ref filteredData, searchStr, categoryId, provinceCityId, districtId, minPrice, maxPrice);
                return filteredData;
            }
            var data = await  _context.Products
                                     .Include(x => x.Category)
                                     .Include(x => x.District)
                                     .ThenInclude(x => x.ProvinceCity)
                                     .OrderBy(x => x.Id)
                                     .ToListAsync();
            _redisCacheService.SetData<List<Product>>("Product", data, expirationTime);
            cacheData = _redisCacheService.GetData<List<Product>>("Product");
            filteredData = cacheData;
            Search(ref filteredData, searchStr, categoryId, provinceCityId, districtId, minPrice, maxPrice);
            return filteredData;
        }

        public async Task<Product> GetByIdWithDetails(int id)
        {
            var list = await GetAllWithDetails("", 0, 0, 0, 0.0, 0.0);
            var data = list.FirstOrDefault(x => x.Id == id);
            return data;
        }
        public async Task<Product> GetOnlyProductById(int id)
        {
            // In the following line, there shoud be AsNoTracking(),Otherwise,there will be an error
            //  when we utilize this method to delete the image of the old product in Controller
            var data = await _context.Products.AsNoTracking()
                      .FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }


    }

}

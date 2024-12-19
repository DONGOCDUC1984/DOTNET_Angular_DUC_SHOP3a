
namespace DOTNET_Angular_DUC_SHOP3a.Repository.Implementation
{
    public class ProvinceCityRepository : GenericRepository<ProvinceCity>, IProvinceCityRepository
    {
        private readonly AppDbContext _context;
        public ProvinceCityRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> AddUpdate(ProvinceCity provinceCity)
        {
            try
            {
                // Add
                if (provinceCity.Id == 0)
                    await _context.ProvinceCities.AddAsync(provinceCity);
                // Update
                else
                    _context.ProvinceCities.Update(provinceCity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}

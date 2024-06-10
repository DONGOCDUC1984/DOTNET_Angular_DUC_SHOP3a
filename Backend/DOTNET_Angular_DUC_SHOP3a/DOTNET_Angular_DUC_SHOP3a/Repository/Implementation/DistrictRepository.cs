
namespace DOTNET_Angular_DUC_SHOP3a.Repository.Implementation
{
    public class DistrictRepository : GenericRepository<District>, IDistrictRepository
    {
        private readonly AppDbContext _context;
        public DistrictRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddUpdate(DistrictAddUpdateDTO modelDTO)
        {
            try
            {
                var provinceCity = await _context.ProvinceCities.FindAsync(modelDTO.ProvinceCityId);
                // Add
                if (modelDTO.Id == 0)
                {
                    // Find a new Id .Without newId, there will be an error.
                    var listId = (from x in _context.Districts
                                  select x.Id).ToList();
                    int newId = listId.Max() + 1;
                    // Create a new district
                    var district = new District()
                    {
                        Id = newId,
                        Name = modelDTO.Name,
                        ProvinceCity = provinceCity,
                    };
                    await _context.Districts.AddAsync(district);
                }
                // Update
                else
                {
                    var district = new District()
                    {
                        Id = modelDTO.Id,
                        Name = modelDTO.Name,
                        ProvinceCity = provinceCity
                    };
                    _context.Districts.Update(district);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<District>> GetAllWithDetails()
        {
            var data = await _context.Districts
                .Include(x => x.ProvinceCity)
                .ToListAsync();
            return data;
        }
    }

}

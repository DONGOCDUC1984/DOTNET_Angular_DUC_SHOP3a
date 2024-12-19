
namespace DOTNET_Angular_DUC_SHOP3a.Repository.Implementation
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> AddUpdate(Category category)
        {
            try
            {   // Add
                if (category.Id == 0)
                   await _context.Categories.AddAsync(category);
                //Update
                else
                    _context.Categories.Update(category);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}

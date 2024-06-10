
namespace DOTNET_Angular_DUC_SHOP3a.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        DbSet<T> dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var data = await dbSet.ToListAsync();
            return data;
        }

        public async Task<T> GetById(int id)
        {
            var data = await dbSet.FindAsync(id);
            return data;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var data = await dbSet.FindAsync(id);
                dbSet.Remove(data);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

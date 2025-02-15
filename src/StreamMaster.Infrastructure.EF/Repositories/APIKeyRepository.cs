using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace StreamMaster.Infrastructure.EF.Repositories
{
    public class APIKeyRepository(IRepositoryContext context) : IAPIKeyRepository
    {
        public async Task AddAsync(APIKey apiKey)
        {
            EntityEntry<APIKey> entityEntry = await context.APIKeys.AddAsync(apiKey);
            _ = await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<APIKey>> GetByUserIdAsync(string userId)
        {
            List<APIKey> listAsync = await context.APIKeys.Where(k => k.UserId == userId && k.IsActive).ToListAsync();
            return listAsync;
        }

        public async Task<APIKey> GetByIdAsync(Guid id)
        {
            return await context.APIKeys.FindAsync(id);
        }

        public async Task UpdateAsync(APIKey apiKey)
        {
            context.APIKeys.Update(apiKey);
            _ = await context.SaveChangesAsync();
        }
    }
}
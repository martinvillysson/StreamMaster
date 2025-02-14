namespace StreamMaster.Domain.Repository
{
    public interface IAPIKeyRepository
    {
        Task AddAsync(APIKey apiKey);

        Task<IEnumerable<APIKey>> GetByUserIdAsync(string userId);

        Task<APIKey> GetByIdAsync(Guid id);

        Task UpdateAsync(APIKey apiKey);
    }
}
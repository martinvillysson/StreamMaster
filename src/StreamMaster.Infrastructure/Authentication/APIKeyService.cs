using StreamMaster.Domain.Repository;

namespace StreamMaster.Infrastructure.Authentication
{
    public class APIKeyService(IAPIKeyRepository repository) : IAPIKeyService
    {
        public async Task<APIKeyResponse> CreateKeyAsync(string username, CreateAPIKeyRequest request)
        {
            APIKey apiKey = new APIKey()
            {
                Id = Guid.NewGuid(),
                Key = Guid.NewGuid().ToString(),
                UserId = username,
                DeviceName = request.DeviceName,
                Scopes = request.Scopes,
                Expiration = request.Expiration,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            await repository.AddAsync(apiKey);
            APIKeyResponse keyAsync = new APIKeyResponse()
            {
                Id = apiKey.Id,
                Key = apiKey.Key,
                DeviceName = apiKey.DeviceName,
                Scopes = apiKey.Scopes,
                Expiration = apiKey.Expiration,
                CreatedAt = apiKey.CreatedAt,
                IsActive = apiKey.IsActive
            };
            return keyAsync;
        }

        public async Task<IEnumerable<APIKeyResponse>> GetKeysAsync(string username)
        {
            IEnumerable<APIKey> keys = await repository.GetByUserIdAsync(username);
            IEnumerable<APIKeyResponse> keysAsync = keys.Select(key => new APIKeyResponse()
            {
                Id = key.Id,
                DeviceName = key.DeviceName,
                Scopes = key.Scopes,
                Expiration = key.Expiration,
                CreatedAt = key.CreatedAt,
                LastUsedAt = key.LastUsedAt,
                IsActive = key.IsActive
            });
            return keysAsync;
        }

        public async Task<bool> RevokeKeyAsync(string username, Guid id)
        {
            APIKey key = await repository.GetByIdAsync(id);
            if (key == null || key.UserId != username)
                return false;
            key.IsActive = false;
            await repository.UpdateAsync(key);
            return true;
        }
    }
}
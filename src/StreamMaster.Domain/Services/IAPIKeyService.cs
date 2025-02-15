namespace StreamMaster.Domain.Services
{
    public interface IAPIKeyService
    {
        Task<APIKeyResponse> CreateKeyAsync(string username, CreateAPIKeyRequest request);

        Task<IEnumerable<APIKeyResponse>> GetKeysAsync(string username);

        Task<bool> RevokeKeyAsync(string username, Guid id);
    }
}
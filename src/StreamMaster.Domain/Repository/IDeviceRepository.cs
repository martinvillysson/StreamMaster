namespace StreamMaster.Domain.Repository
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<Device>> GetByUserIdAsync(string userId);

        Task<Device> GetByIdAsync(Guid id);

        Task DeleteAsync(Device device);

        Task AddOrUpdateDeviceAsync(Device device);
    }
}
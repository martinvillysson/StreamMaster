namespace StreamMaster.Domain.Services
{
    public interface IDeviceService
    {
        Task<IEnumerable<DeviceResponse>> GetDevicesAsync(string username);

        Task<bool> RevokeDeviceAsync(string username, Guid id);
    }
}
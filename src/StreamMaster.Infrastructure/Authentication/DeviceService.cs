using StreamMaster.Domain.Repository;

namespace StreamMaster.Infrastructure.Authentication
{
    public class DeviceService(IDeviceRepository repository) : IDeviceService
    {
        public async Task<IEnumerable<DeviceResponse>> GetDevicesAsync(string username)
        {
            IEnumerable<Device> devices = await repository.GetByUserIdAsync(username);
            IEnumerable<DeviceResponse> devicesAsync = devices.Select(device => new DeviceResponse()
            {
                Id = device.Id,
                DeviceType = device.DeviceType,
                DeviceId = device.DeviceId,
                UserAgent = device.UserAgent,
                IPAddress = device.IPAddress,
                LastActivity = device.LastActivity
            });
            return devicesAsync;
        }

        public async Task<bool> RevokeDeviceAsync(string username, Guid id)
        {
            Device device = await repository.GetByIdAsync(id);
            if (device == null || device.UserId != username)
            {
                return false;
            }
            await repository.DeleteAsync(device);
            return true;
        }
    }
}
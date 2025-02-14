using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace StreamMaster.Infrastructure.EF.Repositories
{
    public class DeviceRepository(IRepositoryContext context) : IDeviceRepository
    {
        public async Task<IEnumerable<Device>> GetByUserIdAsync(string userId)
        {
            return await context.Devices.Where(d => d.UserId == userId).ToListAsync();
        }

        public async Task<Device> GetByIdAsync(Guid id)
        {
            return await context.Devices.FindAsync(id);
        }

        public async Task DeleteAsync(Device device)
        {
            context.Devices.Remove(device);
            _ = await context.SaveChangesAsync();
        }

        public async Task AddOrUpdateDeviceAsync(Device device)
        {
            Device existingDevice = await context.Devices.FirstOrDefaultAsync(d => d.ApiKeyId == device.ApiKeyId && d.DeviceId == device.DeviceId);
            if (existingDevice != null)
            {
                existingDevice.LastActivity = device.LastActivity;
                existingDevice.IPAddress = device.IPAddress;
                existingDevice.UserAgent = device.UserAgent;
                context.Devices.Update(existingDevice);
            }
            else
            {
                EntityEntry<Device> entityEntry = await context.Devices.AddAsync(device);
            }
            _ = await context.SaveChangesAsync();
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StreamMaster.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DevicesController(IDeviceService deviceService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            IEnumerable<DeviceResponse> devices = await deviceService.GetDevicesAsync(User.Identity.Name);
            return Ok(devices);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RevokeDevice(Guid id)
        {
            bool success = await deviceService.RevokeDeviceAsync(User.Identity.Name, id);
            return !success ? NotFound("Device not found or already revoked.") : Ok(new
            {
                Message = "Device access revoked successfully."
            });
        }
    }
}
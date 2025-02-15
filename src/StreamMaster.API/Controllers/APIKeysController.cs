using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StreamMaster.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class APIKeysController(IAPIKeyService apiKeyService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateKey([FromBody] CreateAPIKeyRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.DeviceName))
            {
                return BadRequest("Invalid request.");
            }

            APIKeyResponse result = await apiKeyService.CreateKeyAsync(User.Identity.Name, request);
            return result == null ? StatusCode(500, "Failed to create API key.") : Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetKeys()
        {
            IEnumerable<APIKeyResponse> keys = await apiKeyService.GetKeysAsync(User.Identity.Name);
            return Ok(keys);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RevokeKey(Guid id)
        {
            bool success = await apiKeyService.RevokeKeyAsync(User.Identity.Name, id);
            return !success ? NotFound("API key not found or already revoked.") : Ok(new
            {
                Message = "API key revoked successfully."
            });
        }
    }
}
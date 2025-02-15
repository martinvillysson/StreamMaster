namespace StreamMaster.Domain.Models
{
    public class CreateAPIKeyRequest
    {
        public string DeviceName { get; set; }

        public List<string> Scopes { get; set; }

        public DateTime? Expiration { get; set; }
    }
}
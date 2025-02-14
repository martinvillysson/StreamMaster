namespace StreamMaster.Domain.Models
{
    public class APIKey
    {
        public Guid Id { get; set; }

        public string Key { get; set; }

        public string UserId { get; set; }

        public string DeviceName { get; set; }

        public List<string> Scopes { get; set; }

        public DateTime? Expiration { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastUsedAt { get; set; }

        public bool IsActive { get; set; }
    }
}
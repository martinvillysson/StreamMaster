namespace StreamMaster.Domain.Models
{
    public class DeviceResponse
    {
        public Guid Id { get; set; }

        public string DeviceType { get; set; }

        public string DeviceId { get; set; }

        public string UserAgent { get; set; }

        public string IPAddress { get; set; }

        public DateTime LastActivity { get; set; }
    }
}
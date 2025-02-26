namespace StreamMaster.Streams.Domain.Exceptions
{
    public class SourceBroadcasterNotFoundException : Exception
    {
        public SourceBroadcasterNotFoundException(string message) : base(message)
        {
        }
    }
}
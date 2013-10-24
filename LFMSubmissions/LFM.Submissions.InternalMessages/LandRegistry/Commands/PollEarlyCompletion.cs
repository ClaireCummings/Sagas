namespace LFM.Submissions.InternalMessages.LandRegistry.Commands
{
    public class PollEarlyCompletion : IOutstandingRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string MessageId { get; set; }
    }
}

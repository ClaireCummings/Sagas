namespace LFM.Submissions.InternalMessages.LandRegistry.Commands
{
    public class PollForOutstandingRequests
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ApplicationId { get; set; }
    }
}

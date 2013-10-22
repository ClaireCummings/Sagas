namespace LFM.Submissions.InternalMessages.LandRegistry.Commands
{
    public class StopPollingForOutstandingRequests
    {
        public string ApplicationId { get; set; }
        public string Username { get; set; }
    }
}

namespace LFM.Submissions.InternalMessages.LandRegistry.Messages
{
    public class EdrsAcknowledgementReceived : IEdrsResponseReceived
    {
        public string ApplicationId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

}

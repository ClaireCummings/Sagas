namespace LFM.Submissions.InternalMessages.LandRegistry.Commands
{
    public class PollEdrs : IOutstandingRequest
    {
        public string ApplicationId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string MessageId
        {
            get { return ApplicationId; }
            set { ApplicationId = value; }
        }
    }
}

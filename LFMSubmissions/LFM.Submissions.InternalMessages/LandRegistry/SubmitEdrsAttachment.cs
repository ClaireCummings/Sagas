namespace LFM.Submissions.InternalMessages.LandRegistry
{
    public class SubmitEdrsAttachment 
    {
        public string AttachmentId { get; set; }
        public string ApplicationId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Payload { get; set; }
    }
}

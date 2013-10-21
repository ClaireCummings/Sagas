namespace LFM.Submissions.InternalMessages.LandRegistry.Messages
{
    public interface IEdrsResponseReceived
    {
        string ApplicationId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}
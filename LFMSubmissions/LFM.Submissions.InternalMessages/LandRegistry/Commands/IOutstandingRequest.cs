namespace LFM.Submissions.InternalMessages.LandRegistry.Commands
{
    public interface IOutstandingRequest
    {
        string MessageId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}
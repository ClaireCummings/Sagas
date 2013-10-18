namespace LFM.Submissions.Targets.LandRegistry
{
    public interface IEdrsSender
    {
        string ApplicationId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Payload { get; set; }
        
        string Submit();
    }
}
namespace LFM.Submissions
{
    public class UpdateSubmissionCommand : CreateSubmissionCommand
    {
        public UpdateSubmissionCommand()
        {
        }

        public UpdateSubmissionCommand(Submission submission)
        {
            ApplicationId = submission.ApplicationId;
            AgentUsername = submission.AgentUsername;
            Payload = submission.Payload;
            Status = submission.Status;
        }
    }
}

namespace LFM.Submissions
{
    public interface ISubmissionAdministrationService
    {
        void Create(CreateSubmissionCommand createSubmissionCommand);
        void Update(UpdateSubmissionCommand updateSubmissionCommand);
    }
}

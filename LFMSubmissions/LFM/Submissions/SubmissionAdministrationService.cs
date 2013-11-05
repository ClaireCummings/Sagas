namespace LFM.Submissions
{
    public class SubmissionAdministrationService : ISubmissionAdministrationService
    {
        private readonly ISubmissionRepository _submissionRepository;

        public SubmissionAdministrationService(ISubmissionRepository submissionRepository)
        {
            _submissionRepository = submissionRepository;
        }

        public void Create(CreateSubmissionCommand createSubmissionCommand)
        {
            var submission = new Submission(createSubmissionCommand);
            _submissionRepository.Save(submission);
            _submissionRepository.PersistAll();
        }

        public void Update(UpdateSubmissionCommand updateSubmissionCommand)
        {
            var submission = _submissionRepository.GetById(updateSubmissionCommand.ApplicationId);
            submission.Update(updateSubmissionCommand);
            _submissionRepository.PersistAll();
        }
    }
}

using LFM.Submissions;

namespace LFM.ApplicationServices.Submissions
{
    public class SubmissionService : IHandleQuery<CreateSubmissionQueryResult>,
                                        IHandleQuery<UpdateSubmissionQuery,UpdateSubmissionQueryResult>,
                                        IHandleCommand<CreateSubmissionCommand,CreateSubmissionQueryResult>,
                                        IHandleCommand<UpdateSubmissionCommand, UpdateSubmissionQueryResult>
    {
        private readonly ISubmissionRepository _submissionRepository;
        
        public SubmissionService(ISubmissionRepository submissionRepository)
        {
            _submissionRepository = submissionRepository;
        }

        public CreateSubmissionQueryResult Execute(CreateSubmissionCommand command)
        {
            var submission = new Submission(command);
            _submissionRepository.Save(submission);
            _submissionRepository.PersistAll();
          
            return new CreateSubmissionQueryResult()
                {
                    Command = command
                };
        }

        public UpdateSubmissionQueryResult Execute(UpdateSubmissionCommand command)
        {
            var submission = _submissionRepository.GetById(command.ApplicationId);
            submission.Update(command);
            _submissionRepository.PersistAll();

            return new UpdateSubmissionQueryResult()
                {
                    Command = command
                };
        }

        public CreateSubmissionQueryResult Query()
        {
            return new CreateSubmissionQueryResult();
        }

        public UpdateSubmissionQueryResult Query(UpdateSubmissionQuery query)
        {
            var submission = _submissionRepository.GetById(query.ApplicationId);

            return new UpdateSubmissionQueryResult()
                {
                    Command = new UpdateSubmissionCommand(submission)
                };
        }
    }
}

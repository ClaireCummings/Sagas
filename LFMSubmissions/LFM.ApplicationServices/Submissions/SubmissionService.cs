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
            return new CreateSubmissionQueryResult()
                {
                    Command = command
                };
        }

        public UpdateSubmissionQueryResult Execute(UpdateSubmissionCommand command)
        {
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

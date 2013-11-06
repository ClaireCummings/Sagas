using LFM.Submissions;

namespace LFM.ApplicationServices.Submissions
{
    public class SubmissionService : IHandleQuery<CreateSubmissionQueryResult>,
                                        IHandleQuery<UpdateSubmissionQuery,UpdateSubmissionQueryResult>,
                                        IHandleCommand<CreateSubmissionCommand,CreateSubmissionQueryResult>,
                                        IHandleCommand<UpdateSubmissionCommand, UpdateSubmissionQueryResult>
    {
        private readonly ISubmissionAdministrationService _submissionAdministrationService;
        private readonly ISubmissionRepository _submissionRepository;
        
        public SubmissionService(ISubmissionRepository submissionRepository, ISubmissionAdministrationService submissionAdministrationService )
        {
            _submissionRepository = submissionRepository;
            _submissionAdministrationService = submissionAdministrationService;
        }

        public CreateSubmissionQueryResult Execute(CreateSubmissionCommand command)
        {
            _submissionAdministrationService.Create(command);
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

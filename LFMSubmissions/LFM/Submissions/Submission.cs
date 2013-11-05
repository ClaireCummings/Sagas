using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LFM.Submissions
{
    public class Submission
    {
        public Submission(CreateSubmissionCommand command)
        {
            Populate(command);
        }

        //todo: Include which service (eg Land Registry)
        //todo: Include Laserform user (submitted by whom)

        [Key]
        public string ApplicationId { get; set; }
        public string Payload { get; private set; }
        public string AgentUsername { get; private set; }

        [DefaultValue(SubmissionStatus.Pending)]
        public SubmissionStatus Status { get; set; }

        private void Populate(CreateSubmissionCommand command)
        {
            ApplicationId = command.ApplicationId;
            AgentUsername = command.AgentUsername;
            Payload = command.Payload;
            Status = command.Status;
        }

        public virtual void Update(UpdateSubmissionCommand command)
        {
            Populate(command);
        }
    }
}

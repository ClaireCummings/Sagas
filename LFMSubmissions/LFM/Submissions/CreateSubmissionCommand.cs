using System.ComponentModel.DataAnnotations;

namespace LFM.Submissions
{
    public class CreateSubmissionCommand
    {
        [Required]
        public string ApplicationId { get; set; }
        [Required]
        public string AgentUsername { get; set; }
        [Required]
        public string Payload { get; set; }
        public SubmissionStatus Status { get; set; }
    }
}

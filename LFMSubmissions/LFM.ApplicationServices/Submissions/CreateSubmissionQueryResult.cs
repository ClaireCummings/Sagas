using System.Collections.Generic;
using LFM.Submissions;

namespace LFM.ApplicationServices.Submissions
{
    public class CreateSubmissionQueryResult
    {
        public CreateSubmissionCommand Command { get; set; }

        public CreateSubmissionQueryResult()
        {
            Command = new CreateSubmissionCommand();
        }
    }
}

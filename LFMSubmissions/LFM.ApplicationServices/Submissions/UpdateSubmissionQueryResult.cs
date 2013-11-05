using System.Collections.Generic;
using LFM.Submissions;

namespace LFM.ApplicationServices.Submissions
{
    public class UpdateSubmissionQueryResult
    {
        public UpdateSubmissionCommand Command { get; set; }

        public UpdateSubmissionQueryResult()
        {
            Command = new UpdateSubmissionCommand();
        }
    }
}

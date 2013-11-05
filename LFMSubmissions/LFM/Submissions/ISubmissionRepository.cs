using System.Collections.Generic;

namespace LFM.Submissions
{
    public interface ISubmissionRepository
    {
        Submission GetById(string applicationId);
        void PersistAll();
        void Save(Submission submission);
        IList<Submission> GetAll();
    }
}

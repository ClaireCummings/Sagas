using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LFM.Submissions;

namespace LFM.Infrastructure.Submissions
{
    public class SubmissionRepository : ISubmissionRepository 
    {
        private readonly SubmissionsContext _context;

        public SubmissionRepository(SubmissionsContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
        }

        public void PersistAll()
        {
            _context.SaveChanges();
        }

        public void Save(Submission submission)
        {
            _context.Submissions.Add(submission);
        }

        public IList<Submission> GetAll()
        {
            return _context.Submissions.ToList();
        }
        
        public Submission GetById(string applicationId)
        {
            return _context.Submissions.FirstOrDefault(q => q.ApplicationId == applicationId);
        }
    }
}

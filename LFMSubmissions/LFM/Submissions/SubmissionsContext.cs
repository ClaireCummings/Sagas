using System.Data.Entity;

namespace LFM.Submissions
{
    public class SubmissionsContext : DbContext
    {
        public SubmissionsContext() : base("LFMSubmissions")
        {
        }

        public DbSet<Submission> Submissions { get; set; }
    }
}
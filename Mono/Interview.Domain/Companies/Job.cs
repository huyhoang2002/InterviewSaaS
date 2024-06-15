using Interview.Domain.Seedworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Interview.Domain.Companies
{
    public class Job : EntityBase<Guid>
    {
        public Job()
        {

        }
        public Job(string jobName, string jobDescription, string level, string position, int minExp, int maxExp, Guid companyId, Guid jobCategoryId)
        {
            JobName = jobName;
            JobDescription = jobDescription;
            Level = level;
            Position = position;
            MinExp = minExp;
            MaxExp = maxExp;
            CompanyId = companyId;
            JobCategoryId = jobCategoryId;
        }

        public Job(string jobName, string jobDescription, string level, string position, int minExp, int maxExp)
        {
            JobName = jobName;
            JobDescription = jobDescription;
            Level = level;
            Position = position;
            MinExp = minExp;
            MaxExp = maxExp;
        }

        public Job(Job job)
        {
            JobName = job.JobName;
            JobDescription = job.JobDescription;
            Level = job.Level;
            Position = job.Position;
            MinExp = job.MinExp;
            MaxExp = job.MaxExp;
            CompanyId = job.CompanyId;
            JobCategoryId = job.JobCategoryId;
        }

        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public string Level { get; set; }
        public string Position { get; set; }
        public int MinExp { get; set; }
        public int MaxExp { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public Guid JobCategoryId { get; set; }
        [JsonIgnore]
        public JobCategory JobCategory { get; set; }

        public void Update(Job job)
        {
            JobName = job.JobName;
            JobDescription = job.JobDescription;
            Level = job.Level;
            Position = job.Position;
            MinExp = job.MinExp;
            MaxExp = job.MaxExp;
        }
    }
}

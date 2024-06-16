using Interview.Domain.Seedworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Interview.Domain.Aggregates.Companies
{
    public class JobCategory : EntityBase<Guid>
    {
        public JobCategory()
        {

        }

        public JobCategory(string categoryName, Guid companyId)
        {
            CategoryName = categoryName;
            CompanyId = companyId;
        }

        public string CategoryName { get; set; }
        private List<Job> jobs { get; set; } = new List<Job>();
        public IReadOnlyCollection<Job> Jobs => jobs;

        public Guid CompanyId { get; set; }
        [JsonIgnore]
        public Company Company { get; set; }

        public IEnumerable<Job> GetJobsFromCategory()
        {
            return jobs as IEnumerable<Job>;
        }
    }
}

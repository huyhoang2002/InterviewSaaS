using Interview.Domain.Seedworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Companies
{
    public class JobCategory : EntityBase<Guid>
    {
        public JobCategory(string categoryName, Guid companyId)
        {
            CategoryName = categoryName;
            CompanyId = companyId;
        }

        public string CategoryName { get; set; }
        private List<Job> jobs { get; set; } = new List<Job>();
        public IReadOnlyCollection<Job> Jobs => jobs;

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}

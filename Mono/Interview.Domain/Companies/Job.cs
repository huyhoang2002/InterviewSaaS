using Interview.Domain.Seedworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Companies
{
    public class Job : EntityBase<Guid>
    {
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public string Level { get; set; }
        public string Position { get; set; }
        public int MinExp { get; set; }
        public int MaxExp { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}

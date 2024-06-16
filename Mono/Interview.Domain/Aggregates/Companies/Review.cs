using Interview.Domain.Seedworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Aggregates.Companies
{
    public class Review : EntityBase<Guid>
    {
        public string ReviewBody { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}

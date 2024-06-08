using Interview.Domain.Seedworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Companies
{
    public class Rating : EntityBase<Guid>
    {
        public RatingEnum Rate { get; set; } 
        
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}

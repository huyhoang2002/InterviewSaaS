using Interview.Domain.Seedworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Companies
{
    public class Address : EntityBase<Guid>
    {
        public Address(string street, string district, string city, string province)
        {
            Street = street;
            District = district;
            City = city;
            Province = province;
        }

        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string? Province { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}

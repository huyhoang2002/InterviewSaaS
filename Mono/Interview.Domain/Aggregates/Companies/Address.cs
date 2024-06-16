using Interview.Domain.Seedworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Interview.Domain.Aggregates.Companies
{
    public class Address : EntityBase<Guid>
    {
        public Address(string street, string district, string city, string province, Guid companyId)
        {
            Street = street;
            District = district;
            City = city;
            Province = province;
            CompanyId = companyId;
        }

        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

        public Guid CompanyId { get; set; }
        [JsonIgnore]
        public Company Company { get; set; }
    }
}

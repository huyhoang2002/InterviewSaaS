using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.DTO.QueryDTO
{
    public class AddressDTO
    {
        public AddressDTO(Guid id, string street, string district, string city, string province)
        {
            Id = id;
            Street = street;
            District = district;
            City = city;
            Province = province;
        }
        public Guid Id { get; set; }

        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string? Province { get; set; }
    }
}

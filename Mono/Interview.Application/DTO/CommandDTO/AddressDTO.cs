﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.DTO.CommandDTO
{
    public class AddressDTO
    {
        public AddressDTO(string street, string district, string city, string province)
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
    }
}

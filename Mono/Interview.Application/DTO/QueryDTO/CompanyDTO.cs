using Interview.Application.DTO.CommandDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.DTO.QueryDTO
{
    public record CompanyDTO(
            Guid Id,
            string CompanyName,
            string CompanyLogoUrl,
            string CompanyDescription,
            string CompanyDomain, 
            string CompanyPhoneNumber
        );
}

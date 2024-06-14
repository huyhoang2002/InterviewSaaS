using AutoMapper;
using Interview.Application.DTO.CommandDTO;
using Interview.Application.Features.Commands.Companies;
using Interview.Application.Features.Commands.User;
using Interview.Domain.Aggregates.User;
using Interview.Domain.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, UserCommand>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();
            CreateMap<Company, AddCompanyCommand>().ReverseMap();
            CreateMap<Address, AddCompanyAddressCommand>().ReverseMap();
            CreateMap<Job, JobDTO>().ReverseMap();
        }
    }
}

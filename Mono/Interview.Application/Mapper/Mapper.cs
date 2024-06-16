using AutoMapper;
using Interview.Application.DTO.CommandDTO;
using Interview.Application.DTO.QueryDTO;
using Interview.Application.Features.Commands.Companies;
using Interview.Application.Features.Commands.User;
using Interview.Domain.Aggregates.Companies;
using Interview.Domain.Aggregates.Interviews;
using Interview.Domain.Aggregates.User;
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
            CreateMap<Job, Interview.Application.DTO.CommandDTO.JobDTO>().ReverseMap();
            CreateMap<Job, Interview.Application.DTO.QueryDTO.JobDTO>().ReverseMap();
        }
    }
}

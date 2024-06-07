using AutoMapper;
using Interview.Application.Features.Commands.User;
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
        }
    }
}

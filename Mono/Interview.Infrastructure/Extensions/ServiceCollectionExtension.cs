using Interview.Infrastructure.Persistences.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInterviewDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<InterviewDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MSSqlServer"));
            });
            return service;
        }
    }
}

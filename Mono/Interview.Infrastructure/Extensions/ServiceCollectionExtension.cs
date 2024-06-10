using Interview.Infrastructure.Persistences.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.CQRS.Queries;
using Interview.Infrastructure.UnitOfWork.Interfaces;
using Interview.Infrastructure.Repositories.Interfaces;
using Interview.Infrastructure.Repositories;

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
            //var serviceProvider = service.BuildServiceProvider();
            //using (var context = serviceProvider.GetRequiredService<InterviewDbContext>())
            //{
            //    context.Database.Migrate();
            //}
            return service;
        }

        public static IServiceCollection AddCommandQuery(this IServiceCollection services)
        {
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();
            return services;
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            return services;
        }
    }
}

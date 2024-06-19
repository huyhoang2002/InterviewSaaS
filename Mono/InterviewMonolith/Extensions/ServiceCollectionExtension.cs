using Interview.Domain.Aggregates.Identities;
using Interview.Infrastructure.Persistences.ApplicationDbContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InterviewMonolith.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "allowedOrigin", policy =>
                {
                    policy.WithOrigins("http//localhost:3000").AllowAnyMethod().AllowAnyHeader();
                });
            });
            services.AddIdentity<Account, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<InterviewDbContext>()
                .AddDefaultTokenProviders();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                //.AddGoogle(options =>
                //{
                //    options.SaveTokens = true;
                //    options.ClientId = configuration["Authentication:Google:ClientId"];
                //    options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                //    //options.CallbackPath = "/signin-google";
                //})
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!)),
                        RequireExpirationTime = true,
                    };
                });
            return services;
        }
    }
}

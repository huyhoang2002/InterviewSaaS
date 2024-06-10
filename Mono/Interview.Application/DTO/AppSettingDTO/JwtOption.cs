using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.DTO.AppSettingDTO
{
    public class JwtOption
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int ExpireIn { get; set; }
    }

    public class JwtOptionSetup : IConfigureOptions<JwtOption>
    {
        private readonly IConfiguration _configuration;

        public JwtOptionSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(JwtOption options)
        {
            _configuration.GetSection("JWT").Bind(options);
        }
    }
}

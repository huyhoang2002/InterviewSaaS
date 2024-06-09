using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.DTO.AppSettingDTO
{
    public class UrlOption
    {
        public string DevelopmentUrl { get; set; }
    }
    public class UrlOptionSetup : IConfigureOptions<UrlOption>
    {
        private readonly IConfiguration _configuration;

        public UrlOptionSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(UrlOption options)
        {
            _configuration.GetSection("URL").Bind(options);
        }
    }
}

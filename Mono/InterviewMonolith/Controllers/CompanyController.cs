using Interview.Application.Features.Commands.Companies;
using Interview.Application.Features.Queries.Companies;
using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.CQRS.Queries;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace InterviewMonolith.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public CompanyController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCompanies()
        {
            var getCompaniesQuery = new GetCompaniesQuery();
            var result = await _queryBus.SendAsync(getCompaniesQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] AddCompanyCommand command)
        {
            var result = await _commandBus.SendAsync(command) as CommandResult;
            if (result?.IsSuccess == true)
            {
                return Ok(result);
            } else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("address/{companyId}")]
        public async Task<IActionResult> CreateAddress(Guid companyId, [FromBody] AddCompanyAddressCommand command)
        {
            command.CompanyId = companyId;
            var result = await _commandBus.SendAsync(command) as CommandResult;
            if (result?.IsSuccess == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}

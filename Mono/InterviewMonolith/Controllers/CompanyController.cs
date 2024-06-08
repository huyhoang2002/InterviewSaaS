using Interview.Application.Features.Commands.Companies;
using Interview.Application.Features.Queries.Companies;
using Interview.Infrastructure.CQRS.Commands;
using Interview.Infrastructure.CQRS.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}

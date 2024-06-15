using Interview.Application.DTO.CommandDTO;
using Interview.Application.Features.Commands.Companies;
using Interview.Application.Features.Queries.Companies;
using Interview.Domain.Companies;
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

        [HttpPost]
        [Route("job-category/{companyId}")]
        public async Task<IActionResult> CreateJobCategory(Guid companyId, [FromBody] JobCategoryDTO jobCategoryDTO)
        {
            var command = new AddJobCategoryCommand
            {
                CompanyId = companyId,
                CategoryName = jobCategoryDTO.JobCategoryName
            };
            var result = await _commandBus.SendAsync(command) as CommandResult;
            if (result?.IsSuccess == false)
            {
                return BadRequest(result);
            }
            if (HttpContext.Response.StatusCode == 500)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("job/{companyId}/{jobCategoryId}")]
        public async Task<IActionResult> CreateJob(Guid companyId, Guid jobCategoryId, [FromBody] JobDTO jobDTO)
        {
            var command = new AddJobCommand
            {
                CompanyId = companyId,
                JobCategoryId = jobCategoryId,
                JobDTO = jobDTO
            };
            var result = await _commandBus.SendAsync(command) as CommandResult;
            if (result?.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{companyId}/{jobCategoryId}")]
        public async Task<IActionResult> GetJobsByCategoryId(Guid companyId, Guid jobCategoryId)
        {
            var query = new GetJobByCategoryQuery
            {
                CompanyId = companyId,
                CategoryId = jobCategoryId
            };
            var result = await _queryBus.SendAsync(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("{companyId}")]
        public async Task<IActionResult> GetJobCategoriesByCompanyId(Guid companyId)
        {
            var query = new GetCategoryByCompanyIdQuery
            {
                CompanyId = companyId
            };
            var result = await _queryBus.SendAsync(query);
            return Ok(result);
        }

        [HttpPut]
        [Route("{companyId}/{jobId}")]
        public async Task<IActionResult> UpdateJob(Guid companyId, Guid jobId, [FromBody] JobDTO jobDTO)
        {
            var command = new UpdateJobCommand
            {
                CompanyId = companyId,
                JobId = jobId,
                JobDTO = jobDTO
            };
            var result = await _commandBus.SendAsync(command) as CommandResult;
            if (result?.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("{companyId}")]
        public async Task<IActionResult> DisableCompany(Guid companyId)
        {
            var command = new DeleteCompanyCommand
            {
                CompanyId = companyId
            };
            var result = await _commandBus.SendAsync(command) as CommandResult;
            if (result?.IsSuccess is false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}

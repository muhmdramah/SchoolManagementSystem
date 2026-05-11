using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagementSystem.Api.Controllers.Common;
using SchoolManagementSystem.Core.Features.Students.Commands.Models;
using SchoolManagementSystem.Core.Features.Students.Queriers.Models;
using SchoolManagementSystem.Data.ApplicationMetadata;

namespace SchoolManagementSystem.Api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class StudentController : ApplicationControllerBase
    {
        private const string _globalRateLimiter = "GlobalRateLimiter";
        private const string _singleStudentCacheInvalidator = "single-student";

        public StudentController(IMediator mediator,
            IOutputCacheStore outputCacheStore) : base(mediator, outputCacheStore)
        {
        }

        [HttpGet(Router.StudentRouting.GetAllStudents)]
        [OutputCache(Duration = 120)]
        [EnableRateLimiting(_globalRateLimiter)]
        public async Task<IActionResult> GetStudents()
        {
            var response = await _mediator.Send(new GetAllStudentsQuery());

            if (response == null || !response.Data.Any())
                return NotFound("No students found!");

            return NewResult(response);
        }

        [HttpGet(Router.StudentRouting.GetStudentById)]
        [OutputCache(PolicyName = "CacheSingleStudentResponse")]
        //[OutputCache(Duration = 120, VaryByRouteValueNames = new[] { "id" })]
        [EnableRateLimiting(_globalRateLimiter)]
        public async Task<IActionResult> GetStudent(int id)
        {
            var response = await _mediator.Send(new GetStudentByIdQuery(id));

            if (response == null)
                return NotFound($"Student with id {id} was not found!");

            return NewResult(response);
        }

        [HttpPost(Router.StudentRouting.Create)]
        [EnableRateLimiting(_globalRateLimiter)]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentCommand command)
        {
            var response = await _mediator.Send(command);

            // Invalidate the cache for the deleted student
            await _outputCacheStore.EvictByTagAsync(_singleStudentCacheInvalidator, default);

            return NewResult(response);
        }

        [HttpPut(Router.StudentRouting.Update)]
        [EnableRateLimiting(_globalRateLimiter)]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentCommand command)
        {
            var response = await _mediator.Send(command);

            // Invalidate the cache for the deleted student
            await _outputCacheStore.EvictByTagAsync(_singleStudentCacheInvalidator, default);

            return NewResult(response);
        }

        [HttpDelete(Router.StudentRouting.Delete)]
        [EnableRateLimiting(_globalRateLimiter)]
        public async Task<IActionResult> DeleteStudent([FromBody] DeleteStudentCommand command)
        {
            var response = await _mediator.Send(command);

            // Invalidate the cache for the deleted student
            await _outputCacheStore.EvictByTagAsync(_singleStudentCacheInvalidator, default);

            return NewResult(response);
        }
    }
}

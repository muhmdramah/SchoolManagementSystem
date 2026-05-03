using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagementSystem.Core.Features.Students.Queriers.Models;

namespace SchoolManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOutputCacheStore _outputCacheStore;

        public StudentController(IMediator mediator, IOutputCacheStore outputCacheStore)
        {
            _mediator = mediator;
            _outputCacheStore = outputCacheStore;
        }

        [HttpGet]
        [OutputCache(Duration = 120)]
        [EnableRateLimiting("GlobalRateLimiter")]
        public async Task<IActionResult> GetStudents()
        {
            var response = await _mediator.Send(new GetAllStudentsQuery());

            if (response == null || !response.Data.Any())
                return NotFound("No students found!");

            return Ok(response);
        }

        [HttpGet("{id}")]
        [OutputCache(PolicyName = "CacheSingleStudentResponse")]
        //[OutputCache(Duration = 120, VaryByRouteValueNames = new[] { "id" })]
        [EnableRateLimiting("GlobalRateLimiter")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var response = await _mediator.Send(new GetStudentByIdQuery(id));

            if (response == null)
                return NotFound($"Student with id {id} was not found!");

            return Ok(response);
        }

        //[HttpDelete("{id}")]
        //[EnableRateLimiting("GlobalRateLimiter")]
        //public async Task<IActionResult> DeleteStudent(int id)
        //{
        //    await _studentService.DeleteStudentByIdAsync(id);

        //    // Invalidate the cache for the deleted student
        //    await _outputCacheStore.EvictByTagAsync("single-student", default);

        //    return Ok("Student deleted successfully!");
        //}
    }
}

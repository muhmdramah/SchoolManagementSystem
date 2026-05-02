using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.RateLimiting;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IOutputCacheStore _outputCacheStore;

        public StudentController(IStudentService studentService, IOutputCacheStore outputCacheStore)
        {
            _studentService = studentService;
            _outputCacheStore = outputCacheStore;
        }

        [HttpGet]
        [OutputCache(Duration = 120)]
        [EnableRateLimiting("GlobalRateLimiter")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetStudentsAsync();

            if (students == null || !students.Any())
                return NotFound("No students found!");

            return Ok(students);
        }

        [HttpGet("{id}")]
        [OutputCache(PolicyName = "CacheSingleStudentResponse")]
        //[OutputCache(Duration = 120, VaryByRouteValueNames = new[] { "id" })]
        [EnableRateLimiting("GlobalRateLimiter")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);

            if (student == null)
                return NotFound("Student not found!");

            return Ok(student);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("GlobalRateLimiter")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentService.DeleteStudentByIdAsync(id);

            // Invalidate the cache for the deleted student
            await _outputCacheStore.EvictByTagAsync("single-student", default);

            return Ok("Student deleted successfully!");
        }
    }
}

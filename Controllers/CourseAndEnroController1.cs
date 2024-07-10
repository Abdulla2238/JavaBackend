using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LoginApiTest.Services.Contract;
using System;

namespace LoginApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseAndEnroController1 : ControllerBase
    {
        private readonly ITestLog _testLog;

        public CourseAndEnroController1(ITestLog testLog)
        {
            _testLog = testLog;
        }

        [HttpGet("courses")]
        public IActionResult GetAllCoursesWithEnrollments()
        {
            try
            {
                var courses = _testLog.GetAllCoursesWithEnrollments();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}

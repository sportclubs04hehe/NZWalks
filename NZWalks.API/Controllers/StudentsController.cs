using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        // https://localhost:7010/api/Students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames =
            {
                "Huy", "Hai", "Truong", "Thinh","Thanh"
            };

            return Ok(studentNames);
        }


    }
}

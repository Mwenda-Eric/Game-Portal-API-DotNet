using GamePortalAPI.DTOs.StudentDtos;
using GamePortalAPI.Services.StudentService;

namespace GamePortalAPI.Controllers
{
	[ApiController]
	[Route("/api/v1/[controller]")]
	public class StudentController : ControllerBase
	{
        private readonly IStudentService _studentService;

		public StudentController(IStudentService studentService)
		{
            _studentService = studentService;
		}

        [HttpGet]
        [Route("GetAllStudents")]
        public async Task<ActionResult<ServiceResponse<List<GetStudentResponseDto>>>> GetAllTeachers()
        {
            return Ok(await _studentService.GetAllStudents());
        }

        [HttpPost]
        [Route("CreateStudent")]
        public async Task<ActionResult<ServiceResponse<List<GetStudentResponseDto>>>>
            CreateTeacher(CreateStudentRequestDto createStudentRequestDto)
        {
            return Ok(await _studentService.CreateStudent(createStudentRequestDto));
        }
    }
}


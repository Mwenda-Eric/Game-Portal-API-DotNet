namespace GamePortalAPI.Controllers
{
	[ApiController]
	[Route("/api/v1/[controller]")]
	public class TeachersController : ControllerBase
	{
        private readonly IApiService _apiService;

		public TeachersController(IApiService apiService)
		{
            _apiService = apiService;
		}

        [HttpGet]
        [Route("GetAllTeachers")]
        public async Task<ActionResult<ServiceResponse<List<GetTeacherResponseDto>>>> GetAllTeachers()
        {
            return Ok(await _apiService.GetAllTeachers());
        }

        [HttpGet]
        [Route("GetTeachersWithSubject/{subject}")]
        public async Task<ActionResult<ServiceResponse<List<GetTeacherResponseDto>>>> GetTeachersWithSubject(Subject subject)
        {
            return Ok(await _apiService.GetTeachersWithSubject(subject));
        }

        [HttpPost]
        [Route("CreateTeacher")]
        public async Task<ActionResult<ServiceResponse<List<GetTeacherResponseDto>>>>
            CreateTeacher(AddTeacherRequestDto addTeacherRequestDto)
        {
            return Ok(await _apiService.CreateTeacher(addTeacherRequestDto));
        }

        [HttpPost]
        [Route("CreateNewTeacher")]
        public async Task<ActionResult<ServiceResponse<SingleTeacherResponseDto>>>
            CreateNewTeacher(AddTeacherRequestDto addTeacherRequestDto)
        {
            return Ok(await _apiService.CreateNewTeacher(addTeacherRequestDto));
        }

        [HttpPost]
        [Route("AddQuestionForTeacher")]
        public async Task<ActionResult<ServiceResponse<List<GetTeacherResponseDto>>>>
            AddQuestionForTeacher(AddQuestionRequestDto addquestionRequestDto)
        {
            return Ok(await _apiService.AddQuestionForTeacher(addquestionRequestDto));
        }

        [HttpGet]
        [Route("GetQuestionForTeacher/{teachersName}")]
        public async Task<ActionResult<ServiceResponse<List<GetQuestionResponseDto>>>> GetQuestionForTeacher(string teachersName)
        {
            var response = await _apiService.GetQuestionsForTeacher(teachersName);
            if(response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetSessionQuestionsForTeacher/{teachersId}/{sessionId}/subject")]
        public async Task<ActionResult<ServiceResponse<List<GetQuestionResponseDto>>>>
            GetSessionQuestionsForTeacher(int teachersId, int sessionId, Subject subject)
        {
            var response = await _apiService.GetSessionQuestionsForTeacher(teachersId, sessionId, subject);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }
    }
}


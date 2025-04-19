using System;

namespace GamePortalAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        [Route("GetAllSessions")]
        public async Task<ActionResult<ServiceResponse<List<GetSessionResponseDto>>>> GetAllSessions()
        {
            return Ok(await _sessionService.GetAllSessions());
        }

        [HttpGet]
        [Route("GetSessionsByTeacher/{teacherId}")]
        public async Task<ActionResult<ServiceResponse<List<GetSessionResponseDto>>>> GetSessionsByTeacher(int teacherId)
        {
            return Ok(await _sessionService.GetSessionsByTeacher(teacherId));
        }

        [HttpPost]
        [Route("CreateSession")]
        public async Task<ActionResult<ServiceResponse<GetSessionResponseDto>>>
            CreateSession(CreateSessionRequestDto createSessionRequestDto)
        {
            return Ok(await _sessionService.CreateSession(createSessionRequestDto));
        }
    }
}
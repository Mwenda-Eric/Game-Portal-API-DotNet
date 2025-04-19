using GamePortalAPI.DTOs.AttemptDtos;
using GamePortalAPI.DTOs.ServiceResponse;
using GamePortalAPI.Services.AttemptService;
using Microsoft.AspNetCore.Mvc;

namespace GamePortalAPI.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class StudentAttemptController : ControllerBase
{
    private readonly IStudentAttemptService _studentAttemptService;

    public StudentAttemptController(IStudentAttemptService studentAttemptService)
    {
        _studentAttemptService = studentAttemptService;
    }

    [HttpPost]
    [Route("CreateAttempt")]
    public async Task<ActionResult<ServiceResponse<GetStudentAttemptDto>>> CreateAttempt(
        CreateStudentAttemptDto createStudentAttemptDto)
    {
        return Ok(await _studentAttemptService.CreateAttempt(createStudentAttemptDto));
    }

    [HttpGet]
    [Route("GetAttemptsByStudent/{studentId}")]
    public async Task<ActionResult<ServiceResponse<List<GetStudentAttemptDto>>>> GetAttemptsByStudent(int studentId)
    {
        return Ok(await _studentAttemptService.GetAttemptsByStudent(studentId));
    }

    [HttpGet]
    [Route("GetAttemptsBySession/{sessionId}")]
    public async Task<ActionResult<ServiceResponse<List<GetStudentAttemptDto>>>> GetAttemptsBySession(int sessionId)
    {
        return Ok(await _studentAttemptService.GetAttemptsBySession(sessionId));
    }

    [HttpGet]
    [Route("GetAttemptsByTeacher/{teacherId}")]
    public async Task<ActionResult<ServiceResponse<List<GetStudentAttemptDto>>>> GetAttemptsByTeacher(int teacherId)
    {
        return Ok(await _studentAttemptService.GetAttemptsByTeacher(teacherId));
    }

    [HttpGet]
    [Route("GetStudentSessionResult/{studentId}/{sessionId}")]
    public async Task<ActionResult<ServiceResponse<StudentSessionResultDto>>> GetStudentSessionResult(int studentId, int sessionId)
    {
        return Ok(await _studentAttemptService.GetStudentSessionResult(studentId, sessionId));
    }

    [HttpGet]
    [Route("GetAllStudentResultsForSession/{sessionId}")]
    public async Task<ActionResult<ServiceResponse<List<StudentSessionResultDto>>>> GetAllStudentResultsForSession(int sessionId)
    {
        return Ok(await _studentAttemptService.GetAllStudentResultsForSession(sessionId));
    }
}
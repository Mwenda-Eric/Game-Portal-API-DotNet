using GamePortalAPI.DTOs.AttemptDtos;
using GamePortalAPI.DTOs.ServiceResponse;
using GamePortalAPI.Repositories.AttemptRepository;

namespace GamePortalAPI.Services.AttemptService;

public class StudentAttemptService : IStudentAttemptService
{
    private readonly IStudentAttemptRepository _studentAttemptRepository;

    public StudentAttemptService(IStudentAttemptRepository studentAttemptRepository)
    {
        _studentAttemptRepository = studentAttemptRepository;
    }

    public async Task<ServiceResponse<GetStudentAttemptDto>> CreateAttempt(CreateStudentAttemptDto createStudentAttemptDto)
    {
        return await _studentAttemptRepository.CreateAttempt(createStudentAttemptDto);
    }

    public async Task<ServiceResponse<List<GetStudentAttemptDto>>> GetAttemptsByStudent(int studentId)
    {
        return await _studentAttemptRepository.GetAttemptsByStudent(studentId);
    }

    public async Task<ServiceResponse<List<GetStudentAttemptDto>>> GetAttemptsBySession(int sessionId)
    {
        return await _studentAttemptRepository.GetAttemptsBySession(sessionId);
    }

    public async Task<ServiceResponse<List<GetStudentAttemptDto>>> GetAttemptsByTeacher(int teacherId)
    {
        return await _studentAttemptRepository.GetAttemptsByTeacher(teacherId);
    }

    public async Task<ServiceResponse<StudentSessionResultDto>> GetStudentSessionResult(int studentId, int sessionId)
    {
        return await _studentAttemptRepository.GetStudentSessionResult(studentId, sessionId);
    }

    public async Task<ServiceResponse<List<StudentSessionResultDto>>> GetAllStudentResultsForSession(int sessionId)
    {
        return await _studentAttemptRepository.GetAllStudentResultsForSession(sessionId);
    }
}
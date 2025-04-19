using GamePortalAPI.DTOs.AttemptDtos;
using GamePortalAPI.DTOs.ServiceResponse;

namespace GamePortalAPI.Repositories.AttemptRepository;

public interface IStudentAttemptRepository
{
    Task<ServiceResponse<GetStudentAttemptDto>> CreateAttempt(CreateStudentAttemptDto createStudentAttemptDto);
    Task<ServiceResponse<List<GetStudentAttemptDto>>> GetAttemptsByStudent(int studentId);
    Task<ServiceResponse<List<GetStudentAttemptDto>>> GetAttemptsBySession(int sessionId);
    Task<ServiceResponse<List<GetStudentAttemptDto>>> GetAttemptsByTeacher(int teacherId);
    Task<ServiceResponse<StudentSessionResultDto>> GetStudentSessionResult(int studentId, int sessionId);
    Task<ServiceResponse<List<StudentSessionResultDto>>> GetAllStudentResultsForSession(int sessionId);

}
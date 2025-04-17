using GamePortalAPI.DTOs.StudentDtos;

namespace GamePortalAPI.Services.StudentService;

public interface IStudentService
{
    Task<ServiceResponse<List<GetStudentResponseDto>>> GetAllStudents();
    Task<ServiceResponse<GetStudentResponseDto>> CreateStudent(CreateStudentRequestDto createStudentRequestDto);
}
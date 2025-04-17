using GamePortalAPI.DTOs.StudentDtos;

namespace GamePortalAPI.Repositories.StudentRepository;

public interface IStudentRepository
{
    Task<ServiceResponse<List<GetStudentResponseDto>>> GetAllStudents();
    Task<ServiceResponse<GetStudentResponseDto>> CreateStudent(CreateStudentRequestDto createStudentRequestDto);
}
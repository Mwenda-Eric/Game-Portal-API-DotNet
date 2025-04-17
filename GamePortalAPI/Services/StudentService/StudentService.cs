using GamePortalAPI.DTOs.StudentDtos;
using GamePortalAPI.Repositories.StudentRepository;

namespace GamePortalAPI.Services.StudentService;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    
    public async Task<ServiceResponse<List<GetStudentResponseDto>>> GetAllStudents()
    {
        return await _studentRepository.GetAllStudents();
    }

    public async Task<ServiceResponse<GetStudentResponseDto>> CreateStudent(CreateStudentRequestDto createStudentRequestDto)
    {
        return await _studentRepository.CreateStudent(createStudentRequestDto);
    }
}
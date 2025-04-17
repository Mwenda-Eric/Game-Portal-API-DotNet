using GamePortalAPI.DTOs.StudentDtos;

namespace GamePortalAPI.Repositories.StudentRepository;

public class StudentRepository : IStudentRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public StudentRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ServiceResponse<List<GetStudentResponseDto>>> GetAllStudents()
    {
        var serviceResponse = new ServiceResponse<List<GetStudentResponseDto>>();

        try
        {
            var allStudents = await _context.Students.ToListAsync()
                              ?? throw new Exception("ERROR! Retrieving all Students.");
                    
            serviceResponse.Data = allStudents.Select(student => _mapper.Map<GetStudentResponseDto>(student)).ToList();

            serviceResponse.Message = "Successfully Retrieved all Students";
            serviceResponse.IsSuccessful = true;
            return serviceResponse;
        }
        catch(Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = e.Message;
            serviceResponse.IsSuccessful = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<GetStudentResponseDto>> CreateStudent(CreateStudentRequestDto createStudentRequestDto)
    {
        var serviceResponse = new ServiceResponse<GetStudentResponseDto>();

        // Check if a student with the same playerUniqueId already exists
        var existingStudent = await _context.Students
            .FirstOrDefaultAsync(s => s.PlayerUniqueId == createStudentRequestDto.PlayerUniqueId);
    
        if (existingStudent != null)
        {
            // Student already exists, return the existing student
            var existingStudentDto = _mapper.Map<GetStudentResponseDto>(existingStudent);
            serviceResponse.Data = existingStudentDto;
            serviceResponse.IsSuccessful = true;
            serviceResponse.Message = "Student with this ID already exists.";
        
            return serviceResponse;
        }

        // Create new student if no existing student found
        var student = _mapper.Map<Student>(createStudentRequestDto);
    
        await _context.AddAsync(student);
        await _context.SaveChangesAsync();

        var studentResponse = _mapper.Map<GetStudentResponseDto>(student);

        serviceResponse.Data = studentResponse;
        serviceResponse.IsSuccessful = true;
        serviceResponse.Message = "Student created successfully.";

        return serviceResponse;
    }
}
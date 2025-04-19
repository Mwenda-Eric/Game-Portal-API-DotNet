using AutoMapper;
using GamePortalAPI.Data;
using GamePortalAPI.DTOs.AttemptDtos;
using GamePortalAPI.DTOs.ServiceResponse;
using GamePortalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamePortalAPI.Repositories.AttemptRepository;

public class StudentAttemptRepository : IStudentAttemptRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public StudentAttemptRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<GetStudentAttemptDto>> CreateAttempt(CreateStudentAttemptDto createStudentAttemptDto)
    {
        var serviceResponse = new ServiceResponse<GetStudentAttemptDto>();

        try
        {
            var attempt = _mapper.Map<StudentAttempt>(createStudentAttemptDto);
            attempt.AttemptTime = DateTime.Now;

            await _context.StudentAttempts.AddAsync(attempt);
            await _context.SaveChangesAsync();

            // Get the created attempt with navigation properties
            var createdAttempt = await _context.StudentAttempts
                .Include(a => a.Student)
                .Include(a => a.Question)
                .FirstOrDefaultAsync(a => a.Id == attempt.Id);

            if (createdAttempt == null)
            {
                throw new Exception("Failed to retrieve created attempt");
            }

            var attemptDto = _mapper.Map<GetStudentAttemptDto>(createdAttempt);
            
            serviceResponse.Data = attemptDto;
            serviceResponse.Message = "Student attempt recorded successfully";
            serviceResponse.IsSuccessful = true;
        }
        catch (Exception ex)
        {
            serviceResponse.IsSuccessful = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetStudentAttemptDto>>> GetAttemptsByStudent(int studentId)
    {
        var serviceResponse = new ServiceResponse<List<GetStudentAttemptDto>>();

        try
        {
            var attempts = await _context.StudentAttempts
                .Include(a => a.Student)
                .Include(a => a.Question)
                .Where(a => a.StudentId == studentId)
                .OrderByDescending(a => a.AttemptTime)
                .ToListAsync();

            serviceResponse.Data = attempts.Select(a => _mapper.Map<GetStudentAttemptDto>(a)).ToList();
            serviceResponse.Message = $"Retrieved {attempts.Count} attempts for student ID {studentId}";
            serviceResponse.IsSuccessful = true;
        }
        catch (Exception ex)
        {
            serviceResponse.IsSuccessful = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetStudentAttemptDto>>> GetAttemptsBySession(int sessionId)
    {
        var serviceResponse = new ServiceResponse<List<GetStudentAttemptDto>>();

        try
        {
            var attempts = await _context.StudentAttempts
                .Include(a => a.Student)
                .Include(a => a.Question)
                .Where(a => a.SessionId == sessionId)
                .OrderByDescending(a => a.AttemptTime)
                .ToListAsync();

            serviceResponse.Data = attempts.Select(a => _mapper.Map<GetStudentAttemptDto>(a)).ToList();
            serviceResponse.Message = $"Retrieved {attempts.Count} attempts for session ID {sessionId}";
            serviceResponse.IsSuccessful = true;
        }
        catch (Exception ex)
        {
            serviceResponse.IsSuccessful = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetStudentAttemptDto>>> GetAttemptsByTeacher(int teacherId)
    {
        var serviceResponse = new ServiceResponse<List<GetStudentAttemptDto>>();

        try
        {
            var attempts = await _context.StudentAttempts
                .Include(a => a.Student)
                .Include(a => a.Question)
                .Where(a => a.TeacherId == teacherId)
                .OrderByDescending(a => a.AttemptTime)
                .ToListAsync();

            serviceResponse.Data = attempts.Select(a => _mapper.Map<GetStudentAttemptDto>(a)).ToList();
            serviceResponse.Message = $"Retrieved {attempts.Count} attempts for teacher ID {teacherId}";
            serviceResponse.IsSuccessful = true;
        }
        catch (Exception ex)
        {
            serviceResponse.IsSuccessful = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<StudentSessionResultDto>> GetStudentSessionResult(int studentId, int sessionId)
    {
        var serviceResponse = new ServiceResponse<StudentSessionResultDto>();

        try
        {
            // Get student info
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student == null)
            {
                throw new Exception($"Student with ID {studentId} not found");
            }

            // Get session info
            var session = await _context.Sessions.FirstOrDefaultAsync(s => s.SessionId == sessionId);
            if (session == null)
            {
                throw new Exception($"Session with ID {sessionId} not found");
            }

            // Get attempts for this student in this session
            var attempts = await _context.StudentAttempts
                .Include(a => a.Question)
                .Where(a => a.StudentId == studentId && a.SessionId == sessionId)
                .OrderBy(a => a.AttemptTime)
                .ToListAsync();

            // Count total questions in session
            var totalQuestions = await _context.Questions.CountAsync(q => q.SessionId == sessionId);

            // Create the result DTO
            var result = new StudentSessionResultDto
            {
                StudentId = studentId,
                StudentName = student.PlayerName,
                SessionId = sessionId,
                SessionName = session.SessionName,
                TotalQuestions = totalQuestions,
                CorrectAnswers = attempts.Count(a => a.IsCorrect),
                IncorrectAnswers = attempts.Count(a => !a.IsCorrect),
                Attempts = attempts.Select(a => _mapper.Map<GetStudentAttemptDto>(a)).ToList(),
                CompletedTime = attempts.Any() ? attempts.Max(a => a.AttemptTime) : DateTime.Now
            };

            serviceResponse.Data = result;
            serviceResponse.Message = "Student session result retrieved successfully";
            serviceResponse.IsSuccessful = true;
        }
        catch (Exception ex)
        {
            serviceResponse.IsSuccessful = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<StudentSessionResultDto>>> GetAllStudentResultsForSession(int sessionId)
    {
        var serviceResponse = new ServiceResponse<List<StudentSessionResultDto>>();

        try
        {
            // Get all student IDs who have attempted this session
            var studentIds = await _context.StudentAttempts
                .Where(a => a.SessionId == sessionId)
                .Select(a => a.StudentId)
                .Distinct()
                .ToListAsync();

            var results = new List<StudentSessionResultDto>();

            // Get results for each student
            foreach (var studentId in studentIds)
            {
                var studentResult = await GetStudentSessionResult(studentId, sessionId);
                if (studentResult.IsSuccessful && studentResult.Data != null)
                {
                    results.Add(studentResult.Data);
                }
            }

            serviceResponse.Data = results;
            serviceResponse.Message = $"Retrieved results for {results.Count} students in session ID {sessionId}";
            serviceResponse.IsSuccessful = true;
        }
        catch (Exception ex)
        {
            serviceResponse.IsSuccessful = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}
using System;
using GamePortalAPI.DTOs.ServiceResponse;

namespace GamePortalAPI.Repositories.TeacherRepository
{
	public class TeacherRepository : ITeacherRepository
	{
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public TeacherRepository(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetTeacherResponseDto>>> CreateTeacher(AddTeacherRequestDto addTeacherRequestDto)
        {
            var serviceResponse = new ServiceResponse<List<GetTeacherResponseDto>>();

            var newTeacher = _mapper.Map<Teacher>(addTeacherRequestDto);
            newTeacher.dateCreated = DateTime.Now;
            newTeacher.lastUpdated = DateTime.Now;

            await _context.AddAsync(newTeacher);

            await _context.SaveChangesAsync();

            var databaseTeachers = await _context.Teachers
                .Include(teacher => teacher.AllQuestions)
                .ToListAsync();

            serviceResponse.Data = databaseTeachers.Select(teacher => _mapper.Map<GetTeacherResponseDto>(teacher)).ToList();
            serviceResponse.Message = $"Tr {addTeacherRequestDto.TeachersName} Added Successfully!";
            serviceResponse.IsSuccessful = true;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTeacherResponseDto>>> AddQuestionForTeacher(AddQuestionRequestDto addQuestionRequestDto)
        {
            var serviceResponse = new ServiceResponse<List<GetTeacherResponseDto>>();

            var newQuestion = _mapper.Map<Question>(addQuestionRequestDto);
            newQuestion.dateCreated = DateTime.Now;
            newQuestion.lastUpdated = DateTime.Now;

            var teacherToAddQuestion = await _context.Teachers
                .Where(teacher => teacher.Id == addQuestionRequestDto.TeacherId)
                .FirstAsync();

            await _context.AddAsync(newQuestion);

            await _context.SaveChangesAsync();

            var databaseTeachers = await _context.Teachers
                .Include(teacher => teacher.AllQuestions)
                .ToListAsync();

            serviceResponse.Data = databaseTeachers.Select(teacher => _mapper.Map<GetTeacherResponseDto>(teacher)).ToList();
            serviceResponse.Message = $"Question for Tr {teacherToAddQuestion.TeachersName} Added Successfully!";
            serviceResponse.IsSuccessful = true;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTeacherResponseDto>>> GetAllTeachers()
        {
            var serviceResponse = new ServiceResponse<List<GetTeacherResponseDto>>();

            var databaseTeachers = await _context.Teachers
                .Include(teacher => teacher.AllQuestions)
                .ToListAsync();

            serviceResponse.Data = databaseTeachers.Select(teacher => _mapper.Map<GetTeacherResponseDto>(teacher)).ToList();
            serviceResponse.Message = "Success Retrieving all teachers";
            serviceResponse.IsSuccessful = true;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetQuestionResponseDto>>> GetQuestionsForTeacher(string teachersName)
        {
            var serviceResponse = new ServiceResponse<List<GetQuestionResponseDto>>();
            Teacher teacher = new();

            try
            {
                teacher = await _context.Teachers
                .Where(teacher => teacher.TeachersName == teachersName)
                .Include(questions => questions.AllQuestions)
                .FirstAsync();

                serviceResponse.Data = teacher.AllQuestions?.Select(_mapper.Map<GetQuestionResponseDto>).ToList();
                serviceResponse.Message = $"Successfully retrieved questions for Tr {teachersName}";
                serviceResponse.IsSuccessful = true;
            }
            catch (Exception e)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Unable to Get Questions Error is : " + e.Message;
                serviceResponse.IsSuccessful = false;
                return serviceResponse;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetQuestionResponseDto>>>
            GetSessionQuestionsForTeacher(int teachersId, int sessionId, Subject subject)
        {
            var serviceResponse = new ServiceResponse<List<GetQuestionResponseDto>>();
            try
            {
                var sessionQuestions = await _context.Questions
                    .Where(q => q.TeacherId == teachersId && q.SessionId == sessionId)
                    .ToListAsync();

                if (sessionQuestions is null) throw new Exception("Cannot find questions for specified session and teacher!");

                serviceResponse.Data = sessionQuestions.Select(_mapper.Map<GetQuestionResponseDto>).ToList();
                serviceResponse.Message = $"Successfully retrieved questions for Tr {0}";
                serviceResponse.IsSuccessful = true;
            }
            catch (Exception e)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Unable to Get Questions Error is : " + e.Message;
                serviceResponse.IsSuccessful = false;
                return serviceResponse;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTeacherResponseDto>>> GetTeachersWithSubject(Subject subject)
        {
            var serviceResponse = new ServiceResponse<List<GetTeacherResponseDto>>();

            var databaseTeachers = await _context.Teachers
                //.Include(teacher => teacher.AllQuestions)
                .Where(t => t.AllQuestions.Any(q => q.Subject.Equals(subject)))
                .ToListAsync();

            serviceResponse.Data = databaseTeachers.Select(teacher => _mapper.Map<GetTeacherResponseDto>(teacher)).ToList();
            serviceResponse.Message = "Success Retrieving all teachers";
            serviceResponse.IsSuccessful = true;

            return serviceResponse;
        }

        public async Task<ServiceResponse<SingleTeacherResponseDto>> CreateNewTeacher(AddTeacherRequestDto addTeacherRequestDto)
        {
            var serviceResponse = new ServiceResponse<SingleTeacherResponseDto>();

            // Check if a student with the same playerUniqueId already exists
            var existingTeacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.TeacherUniqueId == addTeacherRequestDto.TeacherUniqueId);
    
            if (existingTeacher != null)
            {
                // Student already exists, return the existing student
                var existingTeacherDto = _mapper.Map<SingleTeacherResponseDto>(existingTeacher);
                serviceResponse.Data = existingTeacherDto;
                serviceResponse.IsSuccessful = true;
                serviceResponse.Message = $"Teacher '{existingTeacherDto.TeachersName}' already exists.";
        
                return serviceResponse;
            }
            
            var newTeacher = _mapper.Map<Teacher>(addTeacherRequestDto);
            newTeacher.dateCreated = DateTime.Now;
            newTeacher.lastUpdated = DateTime.Now;

            await _context.AddAsync(newTeacher);

            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<SingleTeacherResponseDto>(newTeacher);
            serviceResponse.Message = $"Teacher '{newTeacher.TeachersName}' Has been SUCCESSFULLY Created! ";
            serviceResponse.IsSuccessful = true;

            return serviceResponse;
        }
    }
}


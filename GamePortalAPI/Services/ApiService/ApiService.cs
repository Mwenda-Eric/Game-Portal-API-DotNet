
using GamePortalAPI.Models;

namespace GamePortalAPI.Services.ApiService
{
	public class ApiService : IApiService
	{
        private readonly ITeacherRepository _teacherRepository;

		public ApiService(ITeacherRepository teacherRepository)
		{
            _teacherRepository = teacherRepository;
		}

        public async Task<ServiceResponse<List<GetTeacherResponseDto>>> CreateTeacher(AddTeacherRequestDto addTeacherRequestDto)
        {
            return await _teacherRepository.CreateTeacher(addTeacherRequestDto);
        }

        public async Task<ServiceResponse<List<GetTeacherResponseDto>>> AddQuestionForTeacher(AddQuestionRequestDto addQuestionRequestDto)
        {
            return await _teacherRepository.AddQuestionForTeacher(addQuestionRequestDto);
        }

        public async Task<ServiceResponse<List<GetTeacherResponseDto>>> GetAllTeachers()
        {
            return await _teacherRepository.GetAllTeachers();
        }

        public async Task<ServiceResponse<List<GetQuestionResponseDto>>> GetQuestionsForTeacher(string teachersName)
        {
            return await _teacherRepository.GetQuestionsForTeacher(teachersName);
        }

        public async Task<ServiceResponse<List<GetQuestionResponseDto>>>
            GetSessionQuestionsForTeacher(int teachersId, int sessionId, Subject subject)
        {
            return await _teacherRepository.GetSessionQuestionsForTeacher(teachersId, sessionId, subject);
        }

        public async Task<ServiceResponse<List<GetTeacherResponseDto>>> GetTeachersWithSubject(Subject subject)
        {
            return await _teacherRepository.GetTeachersWithSubject(subject);
        }

        public async Task<ServiceResponse<SingleTeacherResponseDto>> CreateNewTeacher(AddTeacherRequestDto addTeacherRequestDto)
        {
            return await _teacherRepository.CreateNewTeacher(addTeacherRequestDto);
        }
    }
}


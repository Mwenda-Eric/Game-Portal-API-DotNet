using System;
namespace GamePortalAPI.Repositories.TeacherRepository
{
	public interface ITeacherRepository
	{
        Task<ServiceResponse<List<GetTeacherResponseDto>>> GetAllTeachers();

        Task<ServiceResponse<List<GetTeacherResponseDto>>> GetTeachersWithSubject(Subject subject);

        Task<ServiceResponse<List<GetTeacherResponseDto>>> CreateTeacher(AddTeacherRequestDto addTeacherRequestDto);

        Task<ServiceResponse<SingleTeacherResponseDto>> CreateNewTeacher(AddTeacherRequestDto addTeacherRequestDto);

        Task<ServiceResponse<List<GetTeacherResponseDto>>> AddQuestionForTeacher(AddQuestionRequestDto addQuestionRequestDto);

        Task<ServiceResponse<List<GetQuestionResponseDto>>> GetQuestionsForTeacher(string teachersName);

        Task<ServiceResponse<List<GetQuestionResponseDto>>>
            GetSessionQuestionsForTeacher(int teachersId, int sessionId, Subject subject);

    }
}


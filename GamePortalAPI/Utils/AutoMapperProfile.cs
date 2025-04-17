
using GamePortalAPI.DTOs.StudentDtos;

namespace GamePortalAPI.Utils
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<AddTeacherRequestDto, Teacher>();
			CreateMap<Teacher, GetTeacherResponseDto>();
			CreateMap<Teacher, SingleTeacherResponseDto>();

			CreateMap<AddQuestionRequestDto, Question>();
			CreateMap<Question, GetQuestionResponseDto>();

			CreateMap<Session, GetSessionResponseDto>();
			CreateMap<CreateSessionRequestDto, Session>();

			CreateMap<CreateStudentRequestDto, Student>();
			CreateMap<Student, GetStudentResponseDto>();

		}
	}
}


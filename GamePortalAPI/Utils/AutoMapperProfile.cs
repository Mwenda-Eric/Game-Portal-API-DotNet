
using GamePortalAPI.DTOs.AttemptDtos;
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
			
			// Student Attempt mappings
			CreateMap<StudentAttempt, GetStudentAttemptDto>()
				.ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.PlayerName))
				.ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question.ActualQuestion))
				.ForMember(dest => dest.SelectedAnswer, opt => opt.MapFrom(src => 
					src.SelectedAnswerIndex == 1 ? src.Question.FirstAnswer :
					src.SelectedAnswerIndex == 2 ? src.Question.SecondAnswer :
					src.SelectedAnswerIndex == 3 ? src.Question.ThirdAnswer : "Invalid selection"));
            
			CreateMap<CreateStudentAttemptDto, StudentAttempt>();

		}
	}
}


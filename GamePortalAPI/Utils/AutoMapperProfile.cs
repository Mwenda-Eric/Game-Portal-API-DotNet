using System;
using AutoMapper;

namespace GamePortalAPI.Utils
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<AddTeacherRequestDto, Teacher>();
			CreateMap<Teacher, GetTeacherResponseDto>();
			CreateMap<AddQuestionRequestDto, Question>();
			CreateMap<Question, GetQuestionResponseDto>();
		}
	}
}


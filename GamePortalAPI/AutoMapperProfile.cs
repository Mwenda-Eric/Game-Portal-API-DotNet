using System;
using AutoMapper;
using GamePortalAPI.DTOs.TeacherDtos;
using GamePortalAPI.Models;

namespace GamePortalAPI
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<AddTeacherRequestDto, Teacher>();
			CreateMap<Teacher, GetTeacherResponseDto>();
		}
	}
}


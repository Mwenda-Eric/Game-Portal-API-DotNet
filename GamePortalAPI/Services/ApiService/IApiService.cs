using System;
using GamePortalAPI.DTOs.ServiceResponse;
using GamePortalAPI.DTOs.TeacherDtos;

namespace GamePortalAPI.Services.ApiService
{
	public interface IApiService
	{
        Task<ServiceResponse<List<GetTeacherResponseDto>>> GetAllTeachers();

        Task<ServiceResponse<List<GetTeacherResponseDto>>> CreateTeacher(AddTeacherRequestDto addTeacherRequestDto);
	}
}


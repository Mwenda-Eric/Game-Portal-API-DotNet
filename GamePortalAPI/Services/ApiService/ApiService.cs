using System;
using GamePortalAPI.DTOs.ServiceResponse;
using GamePortalAPI.DTOs.TeacherDtos;

namespace GamePortalAPI.Services.ApiService
{
	public class ApiService : IApiService
	{
		public ApiService()
		{
		}

        public async Task<ServiceResponse<List<GetTeacherResponseDto>>> CreateTeacher(AddTeacherRequestDto addTeacherRequestDto)
        {
            var serviceResponse = new ServiceResponse<List<GetTeacherResponseDto>>();

            return serviceResponse;
        }
    }
}


using System;
using GamePortalAPI.DTOs.ServiceResponse;
using GamePortalAPI.DTOs.TeacherDtos;
using AutoMapper;
using GamePortalAPI.Models;

namespace GamePortalAPI.Services.ApiService
{
	public class ApiService : IApiService
	{
        public List<Teacher> teachers = new List<Teacher>
        {
            new Teacher(),
            new Teacher
            {
                Id = 1,
                TeachersName = "Mr Mwenda",
                ProfilePictureUrl = "https://azure.profile.picture",
                AllQuestions = new List<Question>(),
                dateCreated = DateTime.Now,
                lastUpdated = DateTime.Now,
                
            },
            new Teacher
            {

            }
        };
        private readonly IMapper _mapper;

		public ApiService(IMapper mapper)
		{
            _mapper = mapper;
		}

        public async Task<ServiceResponse<List<GetTeacherResponseDto>>> CreateTeacher(AddTeacherRequestDto addTeacherRequestDto)
        {
            var serviceResponse = new ServiceResponse<List<GetTeacherResponseDto>>();

            var newTeacher =  _mapper.Map<Teacher>(addTeacherRequestDto);
            teachers.Add(newTeacher);

            serviceResponse.Data = teachers.Select(teacher => _mapper.Map<GetTeacherResponseDto>(teacher)).ToList();
            serviceResponse.Message = "Teacher Added Successfully!";
            serviceResponse.IsSuccessful = true;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTeacherResponseDto>>> GetAllTeachers()
        {
            var serviceResponse = new ServiceResponse<List<GetTeacherResponseDto>>();

            serviceResponse.Data = teachers.Select(teacher => _mapper.Map<GetTeacherResponseDto>(teacher)).ToList();

            return serviceResponse;
        }
    }
}


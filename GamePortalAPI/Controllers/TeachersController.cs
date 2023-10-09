using System;
using GamePortalAPI.Data;
using GamePortalAPI.DTOs.QuestionDtos;
using GamePortalAPI.DTOs.ServiceResponse;
using GamePortalAPI.DTOs.TeacherDtos;
using GamePortalAPI.Services.ApiService;
using Microsoft.AspNetCore.Mvc;

namespace GamePortalAPI.Controllers
{
	[ApiController]
	[Route("/api/v1/[controller]")]
	public class TeachersController : ControllerBase
	{
        private readonly IApiService _apiService;

		public TeachersController(IApiService apiService)
		{
            _apiService = apiService;
		}

        [HttpGet]
        [Route("GetAllTeachers")]
        public async Task<ActionResult<ServiceResponse<List<GetTeacherResponseDto>>>> GetAllTeachers()
        {
            return Ok(await _apiService.GetAllTeachers());
        }

        [HttpPost]
        [Route("CreateTeacher")]
        public async Task<ActionResult<ServiceResponse<List<GetTeacherResponseDto>>>>
            CreateTeacher(AddTeacherRequestDto addTeacherRequestDto)
        {
            return Ok(await _apiService.CreateTeacher(addTeacherRequestDto));
        }

        [HttpPost]
        [Route("AddQuestionForTeacher")]
        public async Task<ActionResult<ServiceResponse<List<GetTeacherResponseDto>>>>
            AddQuestionForTeacher(AddQuestionRequestDto addquestionRequestDto)
        {
            return Ok(await _apiService.AddQuestionForTeacher(addquestionRequestDto));
        }

        [HttpGet]
        [Route("GetQuestionForTeacher/{teachersName}")]
        public async Task<ActionResult<ServiceResponse<List<GetQuestionResponseDto>>>> GetQuestionForTeacher(string teachersName)
        {
            var response = await _apiService.GetQuestionsForTeacher(teachersName);
            if(response.Data is null) return NotFound(response);
            return Ok(response);
        }
    }
}


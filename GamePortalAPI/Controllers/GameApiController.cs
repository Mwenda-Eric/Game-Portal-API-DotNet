using System;
using GamePortalAPI.DTOs.ServiceResponse;
using GamePortalAPI.DTOs.TeacherDtos;
using GamePortalAPI.Services.ApiService;
using Microsoft.AspNetCore.Mvc;

namespace GamePortalAPI.Controllers
{
	[ApiController]
	[Route("/api/v1/[controller]")]
	public class GameApiController : ControllerBase //controller base has no view support.
	{
		private readonly IApiService _apiService;

		public GameApiController(IApiService apiService)
		{
			_apiService = apiService;
		}

		[HttpGet]
		public IActionResult DefaultRoot()
		{
			return Ok("DEFAULT ROOT FOR API.");
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
	}
}


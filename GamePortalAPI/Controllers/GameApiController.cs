﻿namespace GamePortalAPI.Controllers
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

		
	}
}


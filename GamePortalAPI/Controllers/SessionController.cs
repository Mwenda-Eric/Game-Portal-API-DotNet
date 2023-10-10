using System;
namespace GamePortalAPI.Controllers
{
	[ApiController]
	[Route("/api/v1/[controller]")]
	public class SessionController
	{
        private readonly IApiService _apiService;

        public SessionController(IApiService apiService)
        {
            _apiService = apiService;
        }
    }
}


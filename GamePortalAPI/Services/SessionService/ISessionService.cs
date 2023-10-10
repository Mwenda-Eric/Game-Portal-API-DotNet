using System;
namespace GamePortalAPI.Services.SessionService
{
	public interface ISessionService
	{
        Task<ServiceResponse<List<GetSessionResponseDto>>> GetAllSessions();
        Task<ServiceResponse<GetSessionResponseDto>> CreateSession(CreateSessionRequestDto createSessionRequestDto);
    }
}


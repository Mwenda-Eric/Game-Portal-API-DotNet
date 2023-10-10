using System;
namespace GamePortalAPI.Repositories.SessionRepository
{
	public interface ISessionRepository
	{
		Task<ServiceResponse<List<GetSessionResponseDto>>> GetAllSessions();
		Task<ServiceResponse<GetSessionResponseDto>> CreateSession(CreateSessionRequestDto createSessionRequestDto);
	}
}


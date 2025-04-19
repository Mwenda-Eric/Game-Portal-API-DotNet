using System;

namespace GamePortalAPI.Services.SessionService
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<ServiceResponse<GetSessionResponseDto>> CreateSession(CreateSessionRequestDto createSessionRequestDto)
        {
            return await _sessionRepository.CreateSession(createSessionRequestDto);
        }

        public async Task<ServiceResponse<List<GetSessionResponseDto>>> GetAllSessions()
        {
            return await _sessionRepository.GetAllSessions();
        }

        public async Task<ServiceResponse<List<GetSessionResponseDto>>> GetSessionsByTeacher(int teacherId)
        {
            return await _sessionRepository.GetSessionsByTeacher(teacherId);
        }
    }
}
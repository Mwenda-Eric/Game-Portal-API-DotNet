using System;

namespace GamePortalAPI.Repositories.SessionRepository
{
	public class SessionRepository : ISessionRepository
	{
        private readonly DataContext _context;
        private readonly IMapper _mapper;

		public SessionRepository(DataContext context, IMapper mapper)
		{
            _context = context;
            _mapper = mapper;
		}

        public async Task<ServiceResponse<GetSessionResponseDto>> CreateSession(CreateSessionRequestDto createSessionRequestDto)
        {
            return null;
        }

        public async Task<ServiceResponse<List<GetSessionResponseDto>>> GetAllSessions()
        {
            var serviceResponse = new ServiceResponse<List<GetSessionResponseDto>>();

            try
            {
                var allSessions = await _context.Sessions.ToListAsync()
                    ?? throw new Exception("ERROR! Retrieving all Sessions.");
                serviceResponse.Data = allSessions.Select(_mapper.Map<GetSessionResponseDto>).ToList();
                serviceResponse.Message = "Successfully Retrieved all Sessions";
                serviceResponse.IsSuccessful = true;
                return serviceResponse;
            }
            catch(Exception e)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = e.Message;
                serviceResponse.IsSuccessful = false;
                return serviceResponse;
            }
            
        }
    }
}


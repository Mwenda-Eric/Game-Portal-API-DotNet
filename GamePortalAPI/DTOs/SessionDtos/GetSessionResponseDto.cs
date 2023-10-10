using System;
namespace GamePortalAPI.DTOs.SessionDtos
{
	public class GetSessionResponseDto
	{
		public int AssignedSessionId { get; }
		public string SessionName { get; } = string.Empty;

	}
}


using System;
namespace GamePortalAPI.DTOs.SessionDtos
{
	public class GetSessionResponseDto
	{
		public int SessionId { get; set; }
        public int TeacherId { get; set; }
        public string SessionName { get; set; } = "Default Session";
		public Subject SessionSubject { get; set; }
	}
}


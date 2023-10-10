using System;
namespace GamePortalAPI.DTOs.SessionDtos
{
	public class GetSessionResponseDto
	{
		public int SessionId { get; set; }
        public string SessionName { get; set; } = "Default Session";
        public int TeacherId { get; set; }
		public Subject SessionSubject { get; set; }
	}
}


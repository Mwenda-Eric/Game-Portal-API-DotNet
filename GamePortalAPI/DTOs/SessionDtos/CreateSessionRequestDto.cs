using System;
namespace GamePortalAPI.DTOs.SessionDtos
{
	public class CreateSessionRequestDto
	{
		public string SessionName { get; set; } = "Default Questions";
		public int TeachersId { get; set; }
		public Subject SessionSubject { get; set; }
	}
}


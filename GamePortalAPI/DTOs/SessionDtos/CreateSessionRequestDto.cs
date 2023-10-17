﻿using System;
namespace GamePortalAPI.DTOs.SessionDtos
{
	public class CreateSessionRequestDto
	{
		public string SessionName { get; set; } = "Default Questions";
		public int TeacherId { get; set; }
		public Subject SessionSubject { get; set; }
	}
}


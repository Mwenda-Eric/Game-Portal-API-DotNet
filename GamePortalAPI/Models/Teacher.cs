﻿using System;
namespace GamePortalAPI.Models
{
	public class Teacher
	{
		public int Id { get; set; }
		public string TeacherUniqueId { get; set; }
		public string TeachersName { get; set; } = String.Empty;
		public List<Question>? AllQuestions { get; set; }
		public List<Session>? GameSessions { get; set; }
		public string ProfilePictureUrl { get; set; } = String.Empty;

		public DateTime dateCreated { get; set; } = DateTime.Now;
		public DateTime lastUpdated { get; set; } = DateTime.Now;
	}
}


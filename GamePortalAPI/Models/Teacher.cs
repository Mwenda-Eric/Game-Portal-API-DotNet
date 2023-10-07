using System;
namespace GamePortalAPI.Models
{
	public class Teacher
	{
		public int Id;
		public string TeachersName { get; set; } = String.Empty;
		public List<Question>? AllQuestions { get; set; }
		public string ProfilePictureUrl { get; set; } = String.Empty;

		public DateTime dateCreated;
		public DateTime lastUpdated;
	}
}


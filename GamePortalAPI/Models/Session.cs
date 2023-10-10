using System;
namespace GamePortalAPI.Models
{
	public class Session
	{
		public int SessionId { get; set; }
		public string SessionName { get; set; } = String.Empty;
		public List<Question>? SessionQuestions { get; set; }
		public Subject SessionSubject { get; set; } = Subject.ENGLISH;

        public int teacherId;
        public Teacher? teacher;
	}
}


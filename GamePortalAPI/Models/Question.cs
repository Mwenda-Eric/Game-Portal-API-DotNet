using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;


namespace GamePortalAPI.Models
{
	[Table("Question")]
	public class  Question
	{
		public int QuestionId { get; set; }
		public string ActualQuestion { get; set; } = String.Empty;
		public string FirstAnswer { get; set; } = String.Empty;
		public string SecondAnswer { get; set; } = String.Empty;
		public string ThirdAnswer { get; set; } = String.Empty;
		public int correctAnswerIndex { get; set; }
		public Subject Subject { get; set; } = Subject.MATH;

		[JsonIgnore]
		public Teacher? Teacher { get; set; }
		public int? TeacherId { get; set; }

		public Session? GameSession;
		public int SessionId { get; set; }

		public DateTime dateCreated { get; set; }
		public DateTime lastUpdated { get; set; }
	}
}

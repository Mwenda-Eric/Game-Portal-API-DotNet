using System;
using System.ComponentModel.DataAnnotations;

namespace GamePortalAPI.Models
{
	public class Question
	{
		public int QuestionId { get; set; }
		public string ActualQuestion { get; set; } = String.Empty;
		public string FirstAnswer { get; set; } = String.Empty;
		public string SecondAnswer { get; set; } = String.Empty;
		public string ThirdAnswer { get; set; } = String.Empty;
		public int correctAnswerIndex { get; set; }
		public Subject Subject { get; set; } = Subject.MATH;
	}
}


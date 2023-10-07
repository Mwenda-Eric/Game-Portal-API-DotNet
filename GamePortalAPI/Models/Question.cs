using System;
namespace GamePortalAPI.Models
{
	public class Question
	{
		public string ActualQuestion { get; set; } = String.Empty;
		public string FirstAnswer { get; set; } = String.Empty;
		public string SecondAnswer { get; set; } = String.Empty;
		public string ThirdAnswer { get; set; } = String.Empty;
		public int correctAnswerIndex { get; set; }
		public Subject Subject { get; set; } = Subject.MATH;
	}
}


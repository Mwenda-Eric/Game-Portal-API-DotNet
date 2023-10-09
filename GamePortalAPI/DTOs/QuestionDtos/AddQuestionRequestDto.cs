using System;
using GamePortalAPI.Models;

namespace GamePortalAPI.DTOs.QuestionDtos
{
	public class AddQuestionRequestDto
	{
        public int TeacherId { get; set; } = 1;
        public string ActualQuestion { get; set; } = String.Empty;
        public string FirstAnswer { get; set; } = String.Empty;
        public string SecondAnswer { get; set; } = String.Empty;
        public string ThirdAnswer { get; set; } = String.Empty;
        public int correctAnswerIndex { get; set; }
        public Subject Subject { get; set; } = Subject.MATH;
    }
}


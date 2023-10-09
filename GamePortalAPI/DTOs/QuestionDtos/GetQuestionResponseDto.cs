using System;
using GamePortalAPI.Models;

namespace GamePortalAPI.DTOs.QuestionDtos
{
	public class GetQuestionResponseDto
	{
        public string ActualQuestion { get; set; } = String.Empty;
        public string FirstAnswer { get; set; } = String.Empty;
        public string SecondAnswer { get; set; } = String.Empty;
        public string ThirdAnswer { get; set; } = String.Empty;
        public int correctAnswerIndex { get; set; }
        public Subject Subject { get; set; } = Subject.MATH;
    }
}


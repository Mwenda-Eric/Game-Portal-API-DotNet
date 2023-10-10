using System;
using GamePortalAPI.Models;

namespace GamePortalAPI.DTOs.QuestionDtos
{
	public class GetQuestionResponseDto
	{
        public string ActualQuestion { get; set; } = string.Empty;
        public string FirstAnswer { get; set; } = string.Empty;
        public string SecondAnswer { get; set; } = string.Empty;
        public string ThirdAnswer { get; set; } = string.Empty;
        public int correctAnswerIndex { get; set; }
        public Subject Subject { get; set; } = Subject.MATH;
    }
}


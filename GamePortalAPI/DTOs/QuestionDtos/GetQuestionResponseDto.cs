using System;
using GamePortalAPI.Models;

namespace GamePortalAPI.DTOs.QuestionDtos
{
	public class GetQuestionResponseDto
	{
        public string ActualQuestion { get; } = string.Empty;
        public string FirstAnswer { get; } = string.Empty;
        public string SecondAnswer { get; } = string.Empty;
        public string ThirdAnswer { get; } = string.Empty;
        public int correctAnswerIndex { get; }
        public Subject Subject { get; } = Subject.MATH;
    }
}


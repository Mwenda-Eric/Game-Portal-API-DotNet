using System;
using GamePortalAPI.Models;

namespace GamePortalAPI.DTOs.TeacherDtos
{
	public class GetTeacherResponseDto
	{
        public int Id;
        public string TeachersName { get; } = string.Empty;
        public List<Question>? AllQuestions { get; }
        public string ProfilePictureUrl { get; } = string.Empty;
    }
}


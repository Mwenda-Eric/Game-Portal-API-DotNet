using System;
using GamePortalAPI.Models;

namespace GamePortalAPI.DTOs.TeacherDtos
{
	public class GetTeacherResponseDto
	{
        public int Id { get; set; }
        public string TeachersName { get; set; } = string.Empty;
        public List<Question>? AllQuestions { get; set; }
        public string ProfilePictureUrl { get; set; } = string.Empty;
    }
}


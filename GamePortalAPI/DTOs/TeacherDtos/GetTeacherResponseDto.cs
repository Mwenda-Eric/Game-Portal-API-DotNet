using System;
using GamePortalAPI.Models;

namespace GamePortalAPI.DTOs.TeacherDtos
{
	public class GetTeacherResponseDto
	{
        public int Id;
        public string TeachersName { get; set; } = String.Empty;
        public List<Question>? AllQuestions { get; set; }
        public string ProfilePictureUrl { get; set; } = String.Empty;
    }
}


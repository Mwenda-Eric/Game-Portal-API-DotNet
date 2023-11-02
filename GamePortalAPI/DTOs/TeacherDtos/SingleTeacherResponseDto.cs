using System;
namespace GamePortalAPI.DTOs.TeacherDtos
{
	public class SingleTeacherResponseDto
	{
        public int TeacherId { get; set; }
        public string TeachersName { get; set; } = String.Empty;
    }
}


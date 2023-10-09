using System;
namespace GamePortalAPI.DTOs.TeacherDtos
{
	public class TeacherRequestDto
	{
		public int TeacherId { get; set; }
		public string TeachersName { get; set; } = String.Empty;
	}
}


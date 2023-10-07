using System;
namespace GamePortalAPI.DTOs.TeacherDtos
{
	public class AddTeacherRequestDto
	{
        public int Id;
        public string TeachersName { get; set; } = String.Empty;
        public string ProfilePictureUrl { get; set; } = String.Empty;
    }
}


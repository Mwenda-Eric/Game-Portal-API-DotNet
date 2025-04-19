using System;
namespace GamePortalAPI.DTOs.TeacherDtos
{
	public class SingleTeacherResponseDto
	{
		public int Id { get; set; }
        public string TeacherUniqueId { get; set; } = string.Empty;
        public string TeachersName { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;
	}
}


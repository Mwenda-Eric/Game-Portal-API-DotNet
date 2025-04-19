namespace GamePortalAPI.DTOs.AttemptDtos;

public class StudentSessionResultDto
{
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public int SessionId { get; set; }
    public string SessionName { get; set; } = string.Empty;
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
    public int IncorrectAnswers { get; set; }
    public List<GetStudentAttemptDto> Attempts { get; set; } = new List<GetStudentAttemptDto>();
    public DateTime CompletedTime { get; set; }
}
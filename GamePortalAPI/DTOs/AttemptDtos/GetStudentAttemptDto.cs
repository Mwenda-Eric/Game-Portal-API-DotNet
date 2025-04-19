namespace GamePortalAPI.DTOs.AttemptDtos;

public class GetStudentAttemptDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public int QuestionId { get; set; }
    public string Question { get; set; } = string.Empty;
    public string SelectedAnswer { get; set; } = string.Empty;
    public int SelectedAnswerIndex { get; set; }
    public bool IsCorrect { get; set; }
    public DateTime AttemptTime { get; set; }
}
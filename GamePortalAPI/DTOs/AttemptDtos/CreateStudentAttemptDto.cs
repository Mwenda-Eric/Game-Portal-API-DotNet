namespace GamePortalAPI.DTOs.AttemptDtos;

public class CreateStudentAttemptDto
{
    public int StudentId { get; set; }
    public int QuestionId { get; set; }
    public int SessionId { get; set; }
    public int TeacherId { get; set; }
    public int SelectedAnswerIndex { get; set; }
    public bool IsCorrect { get; set; }
}
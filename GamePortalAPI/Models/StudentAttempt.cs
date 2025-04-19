namespace GamePortalAPI.Models;

public class StudentAttempt
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int QuestionId { get; set; }
    public int SessionId { get; set; }
    public int TeacherId { get; set; }
    public int SelectedAnswerIndex { get; set; } // 1, 2, or 3 for which option they picked
    public bool IsCorrect { get; set; }
    public DateTime AttemptTime { get; set; } = DateTime.Now;
    
    // Navigation properties
    public Student Student { get; set; }
    public Question Question { get; set; }
}
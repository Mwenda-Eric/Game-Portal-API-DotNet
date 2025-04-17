namespace GamePortalAPI.Models;

public class Student
{
    public int Id { get; set; } 
    public string PlayerUniqueId { get; set; } = string.Empty;
    public string PlayerName { get; set; } = string.Empty;
    public string PlayerRegNo { get; set; } = string.Empty;
    public string PlayerPicUrl { get; set; } = string.Empty;
}
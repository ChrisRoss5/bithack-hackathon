namespace BddAPI.DTOs.Request;

public class EmailMessage
{
    public string ReceiverMail { get; set; } = null!;
    public string? Subject { get; set; }
    public string Body { get; set; } = null!;
}
using System.ComponentModel.DataAnnotations;

namespace BddAPI.Models;

public class RequestDocument
{
    [Key] public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    [Required] public string Purpose { get; set; }
    [Required] public string LocalBoardName { get; set; }

    [Required] public DateTime DateFrom { get; init; }
    [Required] public DateTime DateTo { get; init; }

    public DateTime DateOfIssue { get; set; } = DateTime.Now;
}
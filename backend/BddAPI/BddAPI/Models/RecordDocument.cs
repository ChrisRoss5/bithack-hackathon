using System.ComponentModel.DataAnnotations;

namespace BddAPI.Models;

public class RecordDocument
{
    [Key] public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    [Required] public string LocalBoardName { get; set; }

    [Required] public string ConditionBefore { get; set; }
    [Required] public string ConditionAfter { get; set; }
    
    [Required] public string DamageDone { get; set; }
    [Required] public string Problem { get; set; }
    
    [Required] public DateTime DateOfInspection { get; set; }
    
    public DateTime DateOfIssue { get; set; } = DateTime.Now;
}
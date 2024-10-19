using BddAPI.Models;

namespace BddAPI.DTOs.Response;

public class RecordResponseDto
{
    public Guid Id { get; set; }
    public Contract Contract { get; set; }
    public string ConditionBefore { get; set; }
    public string ConditionAfter { get; set; }
    public string DamageDone { get; set; }
    public string Problems { get; set; }
    public DateTime CreatedAt { get; set; }
}
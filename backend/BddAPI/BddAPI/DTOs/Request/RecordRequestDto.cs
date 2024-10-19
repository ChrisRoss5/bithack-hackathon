using System.ComponentModel.DataAnnotations;

namespace BddAPI.DTOs.Request;

public class RecordRequestDto
{
    public Guid ContractId { get; set; }
    public Guid UserId { get; set; }
    [Required] public string ConditionBefore { get; set; }
    [Required] public string ConditionAfter { get; set; }
    [Required] public string DamageDone { get; set; }
    [Required] public string Problems { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace BddAPI.DTOs.Request;

public class RecordRequestDto
{
    public Guid ContractId { get; set; }
    public Guid UserId { get; set; }
    public string ConditionBefore { get; set; }
    public string ConditionAfter { get; set; }
    public string DamageDone { get; set; }
    public string Problems { get; set; }
}
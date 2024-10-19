using System.ComponentModel.DataAnnotations;

namespace BddAPI.DTOs.Request;

public class ContractRequestDto
{
    [Required] public Guid UserId { get; set; }
    [Required] public Guid CommunityHomeId { get; set; }
    [Required] public bool IsFree { get; set; }
    [Required] public string LeasePurpose { get; set; }
    public bool UsingCutlery { get; set; }
}
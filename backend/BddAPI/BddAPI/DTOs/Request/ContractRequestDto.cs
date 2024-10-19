using System.ComponentModel.DataAnnotations;

namespace BddAPI.DTOs.Request;

public class ContractRequestDto
{
    [Required] public Guid UserId { get; set; }
    [Required] public Guid CommunityHomeId { get; set; }
}
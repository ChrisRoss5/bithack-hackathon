using System.ComponentModel.DataAnnotations;
using BddAPI.Enum;
using BddAPI.Models;

namespace BddAPI.DTOs.Response;

public class ContractResponseDto
{
    public Guid Id { get; set; }

    public User User { get; set; }
    public Guid UserId { get; set; }

    public CommunityHome CommunityHome { get; set; }
    public Guid CommunityHomeId { get; set; }
    [Required] public bool IsFree { get; set; }

    public ContractStatus Status { get; set; } = ContractStatus.Prepared;

    public DateTime DateOfIssue { get; set; }
}
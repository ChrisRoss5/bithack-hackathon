using System.ComponentModel.DataAnnotations;
using BddAPI.Enum;

namespace BddAPI.Models;

public class Contract
{
    [Key] public Guid Id { get; set; }

    public User User { get; set; }
    public Guid UserId { get; set; }

    public CommunityHome CommunityHome { get; set; }
    public Guid CommunityHomeId { get; set; }

    public ContractStatus Status { get; set; } = ContractStatus.Prepared;

    public DateTime DateOfIssue { get; set; } = DateTime.Now;
}
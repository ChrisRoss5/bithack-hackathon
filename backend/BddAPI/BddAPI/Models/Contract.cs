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

    public bool IsFree { get; set; } = false;
    public string LeasePurpose { get; set; }
    public bool UsingCutlery { get; set; } = false;
    public double Vat { get; set; }
    public ContractStatus Status { get; set; } = ContractStatus.New;

    public DateTime DateOfIssue { get; set; } = DateTime.Now;
}
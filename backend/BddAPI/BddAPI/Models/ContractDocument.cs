using System.ComponentModel.DataAnnotations;
using BddAPI.Enum;

namespace BddAPI.Models;

public class ContractDocument
{
    [Key] public Guid Id { get; set; }

    public User User { get; set; }
    public Guid UserId { get; set; }

    [Required] public string LocalBoardName { get; set; }
    [Required] public string LocalBoardAddress { get; set; }
    [Required] public double LocalBoardArea { get; set; }
    [Required] public string LeasePurpose { get; set; }

    [Required] public DateTime DateFrom { get; init; }
    [Required] public DateTime DateTo { get; init; }

    [Required] public double RentPrice { get; set; }
    [Required] public double Bail { get; set; }
    [Required] public double HomeBills { get; set; }

    [Required] public double Vat { get; set; }

    public ContractStatus Status { get; set; } = ContractStatus.Prepared;

    public DateTime DateOfIssue { get; set; } = DateTime.Now;
}
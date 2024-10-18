using System.ComponentModel.DataAnnotations;

namespace BddAPI.Models;

public class CommunityHome
{
    [Key] public Guid Id { get; init; }

    [Required] [MaxLength(50)] public string Name { get; init; }
    [Required] public double RentPrice { get; set; }
    [Required] [MaxLength(50)] public string Address { get; init; }
    [Required] public double LeaseAmount { get; init; }
    [Required] public double BailAmount { get; init; }
    [Required] public double HomeBills { get; set; }
    [Required] public double Vat { get; set; }
    [Required] public double Area { get; init; }
    public double? CutleryPrice { get; init; }
    [Required] public int Capacity { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.Now;
}
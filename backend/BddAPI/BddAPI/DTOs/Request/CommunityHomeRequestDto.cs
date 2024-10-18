using System.ComponentModel.DataAnnotations;

namespace BddAPI.DTOs.Request;

public class CommunityHomeRequestDto
{
    [Required] [MaxLength(50)] public string Name { get; init; }
    [Required] [MaxLength(50)] public string Address { get; init; }
    [Required] [MaxLength(50)] public double LeaseAmount { get; init; }
    [Required] [MaxLength(50)] public double BailAmount { get; init; }
    [Required] [MaxLength(50)] public double Area { get; init; }
    [MaxLength(50)] public double CutleryPrice { get; init; }
    [Required] [MaxLength(50)] public int Capacity { get; init; }
}
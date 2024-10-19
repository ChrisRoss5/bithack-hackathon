using System.ComponentModel.DataAnnotations;

namespace BddAPI.DTOs.Request;

public class CommunityHomeRequestDto
{
    public string? PictureUrl { get; init; }
    [Required] [MaxLength(50)] public string Name { get; init; }
    [Required] [MaxLength(50)] public string Address { get; init; }
    [Required] public double LeaseAmount { get; init; }
    [Required] public double BailAmount { get; init; }
    [Required] public double Area { get; init; }
    public double? CutleryPrice { get; init; }
    [Required] public bool IsFree { get; init; }
    [Required] public int Capacity { get; init; }
}
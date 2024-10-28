using System.ComponentModel.DataAnnotations;

namespace BddAPI.Models;

public class Role
{
    [Key] public Guid Id { get; init; }
    [Required] [MaxLength(50)] public string Name { get; init; }
    [MaxLength(250)] public string? Description { get; init; }
}
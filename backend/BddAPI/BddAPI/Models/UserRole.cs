using System.ComponentModel.DataAnnotations;

namespace BddAPI.Models;

public class UserRole
{
    [Key] public Guid Id { get; init; }

    public Guid UserId { get; init; }
    public User User { get; init; }

    public Guid RoleId { get; init; }
    public Role Role { get; init; }

    public DateTime AssignedAt { get; init; } = DateTime.Now;
}
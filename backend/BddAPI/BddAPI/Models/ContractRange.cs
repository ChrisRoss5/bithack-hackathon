using System.ComponentModel.DataAnnotations;

namespace BddAPI.Models;

public class ContractRange
{
    [Key] public Guid Id { get; set; }
    [Key] public Guid ContractId { get; set; }
    [Required] public DateTime From { get; set; }
    [Required] public DateTime To { get; set; }
}
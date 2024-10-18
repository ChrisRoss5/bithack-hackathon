using System.ComponentModel.DataAnnotations;

namespace BddAPI.Models;

public class RangeModel
{
    [Key] public Guid Id { get; set; }
    [Required] public DateTime From { get; set; }
    [Required] public DateTime To { get; set; }
}
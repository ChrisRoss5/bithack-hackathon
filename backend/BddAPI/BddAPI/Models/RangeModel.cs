using System.ComponentModel.DataAnnotations;

namespace BddAPI.Models;

public class RangeModel
{
    [Key] public Guid Id { get; set; }
    [Required] public List<DateTime> From { get; set; }
    [Required] public List<DateTime> To { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace BddAPI.Models;

public class ContractRangeModel
{
    [Key] public Guid Id { get; set; }

    public Guid ContractId { get; set; }
    public Contract Contract { get; set; }

    public Guid RangeModelId { get; set; }
    public RangeModel RangeModel { get; set; }
}
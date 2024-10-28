using System.ComponentModel.DataAnnotations;
using BddAPI.Models;

namespace BddAPI.DTOs.Request;

public class ContractRangeRequestDto
{
    [Required] public ContractRequestDto ContractRequest { get; set; }
    [Required] public List<ContractRange> ContractRanges { get; set; }
}
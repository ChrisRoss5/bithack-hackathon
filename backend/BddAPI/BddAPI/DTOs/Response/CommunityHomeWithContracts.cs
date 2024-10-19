using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using BddAPI.DTOs.Response;

namespace BddAPI.Models;

public class CommunityHomeWithContracts
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public double RentPrice { get; set; }
    public string Address { get; init; }
    public double LeaseAmount { get; init; }
    public double BailAmount { get; init; }
    public double HomeBills { get; set; }
    public double Vat { get; set; }
    public double Area { get; init; }
    public double? CutleryPrice { get; init; }
    public int Capacity { get; init; }
    public DateTime CreatedAt { get; init; }

    public required List<ContractForSearch> Contracts { get; set; }
}
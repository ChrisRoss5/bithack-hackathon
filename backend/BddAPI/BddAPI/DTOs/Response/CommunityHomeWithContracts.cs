using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace BddAPI.Models;

public class CommunityHomeWithContracts
{
    public required CommunityHome CommunityHome { get; set; }
    public required List<Contract> Contracts { get; set; }
}
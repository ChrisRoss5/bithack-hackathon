using BddAPI.Models;

namespace BddAPI.DTOs.Response;

public class ContractForSearch
{
    public Guid CommunityHomeId { get; set; }
    public CommunityHome CommunityHome { get; set; }
    
    public List<ContractRange> ContractRanges { get; set; }
}
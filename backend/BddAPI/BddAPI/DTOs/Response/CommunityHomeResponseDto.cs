namespace BddAPI.DTOs.Response;

public class CommunityHomeResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Address { get; init; }
    public double LeaseAmount { get; init; }
    public double BailAmount { get; init; }
    public double Area { get; init; }
    public double CutleryPrice { get; init; }
    public int Capacity { get; init; }
    public DateTime CreatedAt { get; init; }
}
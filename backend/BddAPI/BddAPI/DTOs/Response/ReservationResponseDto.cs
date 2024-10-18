namespace BddAPI.DTOs.Response;

public class ReservationResponseDto
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }
    public Guid CommunityCenterId { get; init; }

    public Guid? ContractDocumentId { get; init; }
    public Guid? RecordDocumentId { get; init; }
    public Guid? RequestDocumentId { get; init; }

    public List<List<DateTime>> ReservationFrom { get; init; }
    public List<List<DateTime>> ReservationTo { get; init; }

    public int? ExpectedNumberOfPeople { get; init; }
    public string? AdditionalNotes { get; init; }

    public DateTime CreatedAt { get; init; }
}
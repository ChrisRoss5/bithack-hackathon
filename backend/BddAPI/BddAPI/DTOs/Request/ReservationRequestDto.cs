using System.ComponentModel.DataAnnotations;

namespace BddAPI.DTOs.Request;

public class ReservationRequestDto
{
    [Required] public Guid UserId { get; init; }

    [Required] public Guid CommunityCenterId { get; init; }

    public Guid? ContractDocumentId { get; init; }

    public Guid? RecordDocumentId { get; init; }

    public Guid? RequestDocumentId { get; init; }

    [Required] public List<List<DateTime>> ReservationFrom { get; init; }

    [Required] public List<List<DateTime>> ReservationTo { get; init; }

    public int? ExpectedNumberOfPeople { get; init; }

    [MaxLength(450)] public string? AdditionalNotes { get; init; }
}
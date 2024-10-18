using System.ComponentModel.DataAnnotations;

namespace BddAPI.Models;

public class Reservation
{
    [Key] public Guid Id { get; init; }

    public Guid UserId { get; init; }
    public User User { get; init; }

    public Guid CommunityCenterId { get; init; }
    public CommunityHome CommunityCenter { get; init; }

    public Guid? ContractDocumentId { get; init; }
    public ContractDocument ContractDocument { get; init; }

    public Guid? RecordDocumentId { get; init; }
    public RecordDocument RecordDocument { get; init; }

    public Guid? RequestDocumentId { get; init; }
    public RequestDocument RequestDocument { get; init; }

    [Required] public List<List<DateTime>> ReservationFrom { get; init; }
    [Required] public List<List<DateTime>> ReservationTo { get; init; }
    public int? ExpectedNumberOfPeople { get; init; }
    [MaxLength(450)] public string? AdditionalNotes { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.Now;
}
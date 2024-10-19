namespace BddAPI.DTOs.Response;

public class UserResponseDto
{
    public Guid Id { get; init; }
    public string FirebaseUid { get; init; }
    public string Username { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Address { get; init; }
    public string City { get; init; }
    public string Oib { get; init; }
    public string Bank { get; init; }
    public string PostalCode { get; init; }
    public string Iban { get; init; }
    public string PhoneNumber { get; init; }
    public DateTime CreatedAt { get; init; }
}
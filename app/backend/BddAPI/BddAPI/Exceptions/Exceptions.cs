namespace BddAPI.Exceptions;

internal class UserException(string message) : BddCustomException(message)
{
    public override string ToString()
    {
        return $"UserException: {Message}";
    }
}

internal class ReservationException(string message) : BddCustomException(message)
{
    public override string ToString()
    {
        return $"ReservationException: {Message}";
    }
}

internal class CommunityCenterException(string message) : BddCustomException(message)
{
    public override string ToString()
    {
        return $"CommunityCenter: {Message}";
    }
}

internal class NotFoundException(string message) : BddCustomException(message)
{
    public override string ToString()
    {
        return $"NotFoundException: {Message}";
    }
}

internal class UnauthorizedException(string message) : BddCustomException(message)
{
    public override string ToString()
    {
        return $"UnauthorizedException: {Message}";
    }
}
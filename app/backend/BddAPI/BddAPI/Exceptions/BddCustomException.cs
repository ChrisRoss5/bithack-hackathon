using System;

namespace BddAPI.Exceptions;

public class BddCustomException(string message) : Exception(message)
{
}
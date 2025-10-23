namespace McpTimeServer.Exceptions;

/// <summary>
/// Base exception for timezone-related errors.
/// </summary>
public abstract class TimeZoneException : Exception
{
    protected TimeZoneException(string message) : base(message)
    {
    }

    protected TimeZoneException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

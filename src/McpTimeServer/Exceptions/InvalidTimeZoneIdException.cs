namespace McpTimeServer.Exceptions;

/// <summary>
/// Exception thrown when a timezone ID is invalid or malformed.
/// </summary>
public class InvalidTimeZoneIdException : TimeZoneException
{
    /// <summary>
    /// Gets the invalid timezone ID that caused the exception.
    /// </summary>
    public string TimeZoneId { get; }

    /// <summary>
    /// Gets the city associated with this timezone, if applicable.
    /// </summary>
    public string? City { get; }

    public InvalidTimeZoneIdException(string timeZoneId)
        : base($"Timezone ID '{timeZoneId}' is invalid or empty.")
    {
        TimeZoneId = timeZoneId;
    }

    public InvalidTimeZoneIdException(string timeZoneId, string city)
        : base($"Timezone ID '{timeZoneId}' for city '{city}' is invalid or empty.")
    {
        TimeZoneId = timeZoneId;
        City = city;
    }

    public InvalidTimeZoneIdException(string timeZoneId, string message, Exception innerException)
        : base(message, innerException)
    {
        TimeZoneId = timeZoneId;
    }
}

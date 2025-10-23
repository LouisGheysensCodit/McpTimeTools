namespace McpTimeServer.Exceptions;

/// <summary>
/// Exception thrown when a city name is invalid or not found in the timezone mapping.
/// </summary>
public class InvalidCityException : TimeZoneException
{
    /// <summary>
    /// Gets the invalid city name that caused the exception.
    /// </summary>
    public string CityName { get; }

    public InvalidCityException(string cityName) 
        : base($"City '{cityName}' is invalid or empty.")
    {
        CityName = cityName;
    }

    public InvalidCityException(string cityName, string message) 
        : base(message)
    {
        CityName = cityName;
    }
}

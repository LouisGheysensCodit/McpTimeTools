namespace McpTimeServer.Exceptions;

/// <summary>
/// Exception thrown when attempting to retrieve a timezone for a city that doesn't exist in the mapping.
/// </summary>
public class CityNotFoundException : TimeZoneException
{
    /// <summary>
    /// Gets the city name that was not found.
    /// </summary>
    public string CityName { get; }

    /// <summary>
    /// Gets a list of available cities, if provided.
    /// </summary>
    public IReadOnlyCollection<string>? AvailableCities { get; }

    public CityNotFoundException(string cityName) 
        : base($"City '{cityName}' not found in timezone mapping.")
    {
        CityName = cityName;
    }

    public CityNotFoundException(string cityName, IReadOnlyCollection<string> availableCities) 
        : base($"City '{cityName}' not found in timezone mapping. Available cities: {string.Join(", ", availableCities)}")
    {
        CityName = cityName;
        AvailableCities = availableCities;
    }
}

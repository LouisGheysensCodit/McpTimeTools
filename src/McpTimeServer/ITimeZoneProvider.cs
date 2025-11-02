namespace McpTimeServer;

/// <summary>
/// Provides timezone information for cities.
/// </summary>
public interface ITimeZoneProvider
{
    /// <summary>
    /// Tries to get the timezone ID for a given city.
    /// </summary>
    bool TryGetTimeZoneId(string city, out string? timezoneId);

    /// <summary>
    /// Gets the timezone ID for a given city.
    /// </summary>
    /// <exception cref="Exceptions.CityNotFoundException">Thrown when the city is not found.</exception>
    string GetTimeZoneId(string city);

    /// <summary>
    /// Gets all available cities in the timezone mapping.
    /// </summary>
    /// <param name="sortOrder">The sort order for the returned cities. Defaults to Alphabetical.</param>
    /// <returns>A read-only collection of city names in the specified order.</returns>
    IReadOnlyCollection<string> GetAvailableCities(CitySortOrder sortOrder = CitySortOrder.Alphabetical);

    /// <summary>
    /// Adds or updates a city-to-timezone mapping.
    /// </summary>
    /// <exception cref="Exceptions.InvalidCityException">Thrown when the city name is invalid.</exception>
    /// <exception cref="Exceptions.InvalidTimeZoneIdException">Thrown when the timezone ID is invalid.</exception>
    void AddOrUpdateCity(string city, string timezoneId);
}

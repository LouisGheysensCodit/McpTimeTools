using McpTimeServer.Exceptions;

namespace McpTimeServer;

/// <summary>
/// Default implementation of timezone provider with common city mappings.
/// </summary>
public class TimeZoneProvider : ITimeZoneProvider
{
    private readonly Dictionary<string, string> _cityTimeZones;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeZoneProvider"/> class with default city-to-timezone mappings.
    /// </summary>
    public TimeZoneProvider()
    {
        _cityTimeZones = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Cancun", "America/Cancun" },
            { "Mexico City", "America/Mexico_City" },
            { "New York", "America/New_York" },
            { "London", "Europe/London" },
            { "Tokyo", "Asia/Tokyo" }
        };
    }

    /// <inheritdoc/>
    public bool TryGetTimeZoneId(string city, out string? timezoneId)
    {
        return _cityTimeZones.TryGetValue(city, out timezoneId);
    }

    /// <inheritdoc/>
    public string GetTimeZoneId(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new InvalidCityException(city ?? string.Empty);
        }

        if (!_cityTimeZones.TryGetValue(city, out var timezoneId))
        {
            throw new CityNotFoundException(city, GetAvailableCities());
        }

        return timezoneId;
    }

    /// <inheritdoc/>
    public IReadOnlyCollection<string> GetAvailableCities(CitySortOrder sortOrder = CitySortOrder.Alphabetical)
    {
        var cities = _cityTimeZones.Keys;

        return sortOrder switch
        {
            CitySortOrder.None => cities.ToList().AsReadOnly(),
            CitySortOrder.Alphabetical => cities.Order().ToList().AsReadOnly(),
            CitySortOrder.AlphabeticalDescending => cities.OrderDescending().ToList().AsReadOnly(),
            _ => throw new InvalidCitySortOrderException(sortOrder)
        };
    }

    /// <inheritdoc/>
    public void AddOrUpdateCity(string city, string timezoneId)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new InvalidCityException(city ?? string.Empty, "City cannot be null or empty.");
        }

        if (string.IsNullOrWhiteSpace(timezoneId))
        {
            throw new InvalidTimeZoneIdException(timezoneId ?? string.Empty, city);
        }

        _cityTimeZones[city] = timezoneId;
    }
}

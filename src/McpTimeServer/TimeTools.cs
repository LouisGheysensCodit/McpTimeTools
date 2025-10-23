using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Globalization;

namespace McpTimeServer;

/// <summary>
/// Provides utility methods for retrieving current and city-specific local times.
/// </summary>
[McpServerToolType]
public class TimeTools
{
    private readonly ITimeZoneProvider _timeZoneProvider;
    private readonly TimeProvider _timeProvider;

    public TimeTools(ITimeZoneProvider timeZoneProvider, TimeProvider? timeProvider = null)
    {
        _timeZoneProvider = timeZoneProvider ?? throw new ArgumentNullException(nameof(timeZoneProvider));
        _timeProvider = timeProvider ?? TimeProvider.System;
    }

    /// <summary>
    /// Gets the current UTC time as a string.
    /// </summary>
    /// <returns>The current UTC time.</returns>
    [McpServerTool, Description("Gets the current time.")]
    public string GetCurrentTime()
    {
        return _timeProvider.GetUtcNow().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Gets the local time for the specified city based on predefined time zones.
    /// </summary>
    /// <param name="city">The name of the city to get the local time for.</param>
    /// <returns>
    /// A formatted string containing the city name, its local time, and timezone ID.  
    /// Returns an error message if the city or timezone is not found.
    /// </returns>
    [McpServerTool, Description("Gets local time for a given city name.")]
    public string GetLocalTime(string city)
    {
        return FormatLocalTime(city);
    }

    private string FormatLocalTime(string city)
    {
        if (!_timeZoneProvider.TryGetTimeZoneId(city, out var timezoneId) || timezoneId == null)
        {
            return $"City '{city}' not found in timezone mapping.";
        }

        try
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            var localTime = TimeZoneInfo.ConvertTime(_timeProvider.GetUtcNow(), tz);
            return $"{city}: {localTime:yyyy-MM-dd HH:mm:ss} ({timezoneId})";
        }
        catch (TimeZoneNotFoundException)
        {
            return $"Timezone '{timezoneId}' not found.";
        }
        catch (InvalidTimeZoneException)
        {
            return $"Timezone '{timezoneId}' is invalid.";
        }
    }

    /// <summary>
    /// Adds or updates a city-to-timezone mapping.
    /// </summary>
    public void AddOrUpdateCityTimeZone(string city, string timezoneId)
    {
        _timeZoneProvider.AddOrUpdateCity(city, timezoneId);
    }
}
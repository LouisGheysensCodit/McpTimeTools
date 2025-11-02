namespace McpTimeServer.Tests;

/// <summary>
/// Unit tests for TimeTools and TimeZoneProvider classes.
/// </summary>
public class TimeToolTests
{
    /// <summary>
    /// Tests that GetCurrentTime returns the current UTC time in the correct format.
    /// </summary>
    [Fact]
    public void GetCurrentTime_ReturnsFormattedUtcTime()
    {
        // Arrange
        var timeZoneProvider = new TimeZoneProvider();
        var timeTools = new TimeTools(timeZoneProvider);

        // Act
        var result = timeTools.GetCurrentTime();

        // Assert
        Assert.NotNull(result);
        Assert.Matches(@"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}", result);
    }

    /// <summary>
    /// Tests that GetLocalTime returns the correct local time for a valid city.
    /// </summary>
    [Fact]
    public void GetLocalTime_WithValidCity_ReturnsFormattedTime()
    {
        // Arrange
        var timeZoneProvider = new TimeZoneProvider();
        var timeTools = new TimeTools(timeZoneProvider);

        // Act
        var result = timeTools.GetLocalTime("Tokyo");

        // Assert
        Assert.Contains("Tokyo:", result);
        Assert.Contains("Asia/Tokyo", result);
    }

    /// <summary>
    /// Tests that GetLocalTime returns an error message for an invalid city.
    /// </summary>
    [Fact]
    public void GetLocalTime_WithInvalidCity_ReturnsErrorMessage()
    {
        // Arrange
        var timeZoneProvider = new TimeZoneProvider();
        var timeTools = new TimeTools(timeZoneProvider);

        // Act
        var result = timeTools.GetLocalTime("InvalidCity");

        // Assert
        Assert.Contains("not found in timezone mapping", result);
    }
}
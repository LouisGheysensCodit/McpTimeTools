using McpTimeServer.Exceptions;

namespace McpTimeServer.Tests;

/// <summary>
/// Unit tests for the <see cref="TimeZoneProvider"/> class.
/// </summary>
public class TimeZoneProviderTests
{
    /// <summary>
    /// Verifies that TryGetTimeZoneId returns true and the correct timezone ID when given a valid city name.
    /// </summary>
    [Fact]
    public void TryGetTimeZoneId_WithValidCity_ReturnsTrue()
    {
        // Arrange
        var provider = new TimeZoneProvider();

        // Act
        var result = provider.TryGetTimeZoneId("London", out var timezoneId);

        // Assert
        Assert.True(result);
        Assert.Equal("Europe/London", timezoneId);
    }

    /// <summary>
    /// Verifies that TryGetTimeZoneId returns false when given a city name that does not exist in the mapping.
    /// </summary>
    [Fact]
    public void TryGetTimeZoneId_WithInvalidCity_ReturnsFalse()
    {
        // Arrange
        var provider = new TimeZoneProvider();

        // Act
        var result = provider.TryGetTimeZoneId("InvalidCity", out var timezoneId);

        // Assert
        Assert.False(result);
    }

    /// <summary>
    /// Verifies that TryGetTimeZoneId performs case-insensitive city name matching.
    /// </summary>
    [Fact]
    public void TryGetTimeZoneId_IsCaseInsensitive()
    {
        // Arrange
        var provider = new TimeZoneProvider();

        // Act
        var result = provider.TryGetTimeZoneId("TOKYO", out var timezoneId);

        // Assert
        Assert.True(result);
        Assert.Equal("Asia/Tokyo", timezoneId);
    }

    /// <summary>
    /// Verifies that GetTimeZoneId returns the correct timezone ID when given a valid city name.
    /// </summary>
    [Fact]
    public void GetTimeZoneId_WithValidCity_ReturnsTimeZoneId()
    {
        // Arrange
        var provider = new TimeZoneProvider();

        // Act
        var result = provider.GetTimeZoneId("London");

        // Assert
        Assert.Equal("Europe/London", result);
    }

    /// <summary>
    /// Verifies that GetTimeZoneId throws a CityNotFoundException with available cities when given an invalid city name.
    /// </summary>
    [Fact]
    public void GetTimeZoneId_WithInvalidCity_ThrowsCityNotFoundException()
    {
        // Arrange
        var provider = new TimeZoneProvider();

        // Act & Assert
        var exception = Assert.Throws<CityNotFoundException>(() => provider.GetTimeZoneId("InvalidCity"));
        Assert.Equal("InvalidCity", exception.CityName);
        Assert.NotNull(exception.AvailableCities);
        Assert.Contains("Tokyo", exception.AvailableCities);
    }

    /// <summary>
    /// Verifies that GetTimeZoneId throws an InvalidCityException when given an empty, whitespace, or null city name.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void GetTimeZoneId_WithEmptyCity_ThrowsInvalidCityException(string city)
    {
        // Arrange
        var provider = new TimeZoneProvider();

        // Act & Assert
        var exception = Assert.Throws<InvalidCityException>(() => provider.GetTimeZoneId(city));
        Assert.NotNull(exception.CityName);
    }

    /// <summary>
    /// Verifies that GetAvailableCities returns all default cities configured in the provider.
    /// </summary>
    [Fact]
    public void GetAvailableCities_ReturnsAllCities()
    {
        // Arrange
        var provider = new TimeZoneProvider();

        // Act
        var cities = provider.GetAvailableCities();

        // Assert
        Assert.NotEmpty(cities);
        Assert.Contains("Tokyo", cities);
        Assert.Contains("London", cities);
        Assert.Contains("New York", cities);
        Assert.Equal(5, cities.Count); // Default cities
    }

    /// <summary>
    /// Verifies that GetAvailableCities returns a read-only collection to prevent external modification.
    /// </summary>
    [Fact]
    public void GetAvailableCities_ReturnsReadOnlyCollection()
    {
        // Arrange
        var provider = new TimeZoneProvider();

        // Act
        var cities = provider.GetAvailableCities();

        // Assert
        Assert.IsAssignableFrom<IReadOnlyCollection<string>>(cities);
    }

    /// <summary>
    /// Verifies that AddOrUpdateCity throws an InvalidCityException when given an empty, whitespace, or null city name.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void AddOrUpdateCity_WithInvalidCity_ThrowsInvalidCityException(string city)
    {
        // Arrange
        var provider = new TimeZoneProvider();

        // Act & Assert
        var exception = Assert.Throws<InvalidCityException>(() => provider.AddOrUpdateCity(city, "Europe/Paris"));
        Assert.NotNull(exception.CityName);
    }

    /// <summary>
    /// Verifies that AddOrUpdateCity throws an InvalidTimeZoneIdException when given an empty, whitespace, or null timezone ID.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void AddOrUpdateCity_WithInvalidTimezoneId_ThrowsInvalidTimeZoneIdException(string timezoneId)
    {
        // Arrange
        var provider = new TimeZoneProvider();

        // Act & Assert
        var exception = Assert.Throws<InvalidTimeZoneIdException>(() => provider.AddOrUpdateCity("Paris", timezoneId));
        Assert.NotNull(exception.TimeZoneId);
        Assert.Equal("Paris", exception.City);
    }

    /// <summary>
    /// Verifies that AddOrUpdateCity successfully adds a new city-to-timezone mapping when given valid inputs.
    /// </summary>
    [Fact]
    public void AddOrUpdateCity_WithValidInputs_AddsCity()
    {
        // Arrange
        var provider = new TimeZoneProvider();

        // Act
        provider.AddOrUpdateCity("Paris", "Europe/Paris");
        var result = provider.GetTimeZoneId("Paris");

        // Assert
        Assert.Equal("Europe/Paris", result);
    }

    /// <summary>
    /// Verifies that AddOrUpdateCity updates the timezone ID when called with an existing city name.
    /// </summary>
    [Fact]
    public void AddOrUpdateCity_WithExistingCity_UpdatesTimezone()
    {
        // Arrange
        var provider = new TimeZoneProvider();
        provider.AddOrUpdateCity("TestCity", "America/New_York");

        // Act
        provider.AddOrUpdateCity("TestCity", "Europe/London");
        var result = provider.GetTimeZoneId("TestCity");

        // Assert
        Assert.Equal("Europe/London", result);
    }
}

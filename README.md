# McpTimeTools

[![.NET 8](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/download)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

A Model Context Protocol (MCP) server for current and local time management across multiple cities and timezones.

## ?? Overview

McpTimeTools is a .NET 8 MCP server that provides timezone-aware time utilities. It supports retrieving current UTC time and local times for various cities worldwide, with an extensible architecture for adding custom city-timezone mappings.

## ? Features

- **Current UTC Time**: Get the current time in UTC format
- **City-Based Local Time**: Retrieve local time for predefined cities
- **Extensible Timezone Mapping**: Add or update custom city-timezone mappings
- **Robust Error Handling**: Custom exception types for clear error diagnostics
- **Type-Safe**: Built with C# 12 and nullable reference types
- **Dependency Injection**: Modern .NET patterns with DI support
- **Fully Tested**: Comprehensive unit test coverage

## ??? Default Supported Cities

- **Cancun** (`America/Cancun`)
- **Mexico City** (`America/Mexico_City`)
- **New York** (`America/New_York`)
- **London** (`Europe/London`)
- **Tokyo** (`Asia/Tokyo`)

## ?? Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later

### Installation

Clone the repository:

```bash
git clone https://github.com/LouisGheysensCodit/McpTimeTools.git
cd McpTimeTools/src/McpTimeServer
```

### Build

```bash
dotnet build
```

### Run

```bash
dotnet run
```

### Run Tests

```bash
dotnet test
```

## ?? Usage

### MCP Tools

The server exposes the following MCP tools:

#### `GetCurrentTime`
Gets the current UTC time.

**Returns**: Current UTC time in `yyyy-MM-dd HH:mm:ss` format

**Example**:
```
2024-01-15 14:30:45
```

#### `GetLocalTime`
Gets the local time for a specified city.

**Parameters**:
- `city` (string): Name of the city

**Returns**: Formatted string with city name, local time, and timezone ID

**Example**:
```
Tokyo: 2024-01-15 23:30:45 (Asia/Tokyo)
```

### Programmatic Usage

```csharp
// Create a timezone provider
var timeZoneProvider = new TimeZoneProvider();

// Get timezone ID for a city
var timezoneId = timeZoneProvider.GetTimeZoneId("London");
// Returns: "Europe/London"

// Try to get timezone ID
if (timeZoneProvider.TryGetTimeZoneId("Paris", out var tz))
{
    Console.WriteLine($"Timezone: {tz}");
}

// Add a custom city
timeZoneProvider.AddOrUpdateCity("Paris", "Europe/Paris");

// Get all available cities
var cities = timeZoneProvider.GetAvailableCities();
```

## ??? Architecture

### Project Structure

```
McpTimeServer/
??? Program.cs                          # Application entry point
??? TimeTools.cs                        # MCP tool implementations
??? ITimeZoneProvider.cs                # Timezone provider interface
??? TimeZoneProvider.cs                 # Timezone provider implementation
??? Exceptions/
?   ??? TimeZoneException.cs            # Base exception
?   ??? InvalidCityException.cs         # Invalid city name exception
?   ??? InvalidTimeZoneIdException.cs   # Invalid timezone ID exception
?   ??? CityNotFoundException.cs        # City not found exception
??? McpTimeServer.Tests/
    ??? TimeToolsTests.cs               # TimeTools unit tests
    ??? TimeZoneProviderTests.cs        # TimeZoneProvider unit tests
```

### Key Components

- **`TimeTools`**: MCP server tool class providing time-related operations
- **`ITimeZoneProvider`**: Interface for timezone mapping operations
- **`TimeZoneProvider`**: Default implementation with predefined cities
- **Custom Exceptions**: Domain-specific exceptions for better error handling

### Exception Hierarchy

```
Exception
??? TimeZoneException (abstract base)
    ??? InvalidCityException
    ??? InvalidTimeZoneIdException
    ??? CityNotFoundException
```

## ??? Technologies

- **.NET 8**: Target framework
- **C# 12**: Language version with modern features
- **OllamaSharp.ModelContextProtocol**: MCP server implementation
- **Microsoft.Extensions.Hosting**: Hosting infrastructure
- **xUnit**: Testing framework

## ?? Testing

The project includes comprehensive unit tests covering:

- ? Valid and invalid city lookups
- ? Case-insensitive city matching
- ? Custom exception scenarios
- ? Available cities retrieval
- ? City mapping updates
- ? Edge cases (null, empty, whitespace)

**Test Coverage**: 22 tests, all passing ?

## ?? Examples

### Error Handling with Custom Exceptions

```csharp
try
{
    var tz = provider.GetTimeZoneId("InvalidCity");
}
catch (CityNotFoundException ex)
{
    Console.WriteLine($"City '{ex.CityName}' not found!");
    Console.WriteLine($"Available: {string.Join(", ", ex.AvailableCities)}");
}
catch (InvalidCityException ex)
{
    Console.WriteLine($"Invalid city name: {ex.CityName}");
}
```

### Adding Custom Cities

```csharp
var provider = new TimeZoneProvider();

// Add a new city
provider.AddOrUpdateCity("Berlin", "Europe/Berlin");
provider.AddOrUpdateCity("Sydney", "Australia/Sydney");

// Use the new cities
var berlinTime = provider.GetTimeZoneId("Berlin");
```

## ?? Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request
## ?? Author

**Louis Gheysens**

- GitHub: [@LouisGheysensCodit](https://github.com/LouisGheysensCodit)

## ?? Acknowledgments

- Built with [OllamaSharp.ModelContextProtocol](https://www.nuget.org/packages/OllamaSharp.ModelContextProtocol/)
- Powered by [.NET 8](https://dotnet.microsoft.com/)

---

? If you find this project useful, please consider giving it a star!

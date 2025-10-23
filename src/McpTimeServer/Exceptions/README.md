# Custom Exceptions

This directory contains domain-specific exceptions for the MCP Time Server.

## Exception Hierarchy

```
Exception
??? TimeZoneException (abstract base)
    ??? InvalidCityException
    ??? InvalidTimeZoneIdException
    ??? CityNotFoundException
```

## Exception Details

### `TimeZoneException`
Abstract base class for all timezone-related exceptions. Provides a common base type for catching any timezone-related error.

### `InvalidCityException`
Thrown when a city name is invalid, null, or empty.

**Properties:**
- `CityName`: The invalid city name that caused the exception

**Usage:**
```csharp
throw new InvalidCityException(""); // City is empty
throw new InvalidCityException(null, "Custom message");
```

### `InvalidTimeZoneIdException`
Thrown when a timezone ID is invalid, null, or empty.

**Properties:**
- `TimeZoneId`: The invalid timezone ID
- `City`: The associated city name (optional)

**Usage:**
```csharp
throw new InvalidTimeZoneIdException(""); // Timezone ID is empty
throw new InvalidTimeZoneIdException("", "Paris"); // Timezone ID is empty for Paris
```

### `CityNotFoundException`
Thrown when attempting to retrieve a timezone for a city that doesn't exist in the mapping.

**Properties:**
- `CityName`: The city that was not found
- `AvailableCities`: List of available cities (optional, helpful for error messages)

**Usage:**
```csharp
throw new CityNotFoundException("UnknownCity");
throw new CityNotFoundException("UnknownCity", availableCities);
```

## Benefits

1. **Type Safety**: Catch specific exception types instead of generic exceptions
2. **Rich Context**: Each exception carries relevant data (city name, timezone ID, etc.)
3. **Better Error Messages**: Automatically generated messages with helpful context
4. **Testability**: Easy to test for specific error conditions
5. **API Clarity**: Method signatures clearly indicate what exceptions can be thrown

# Custom Exception Implementation Summary

## ? What Was Added

### 1. Exception Hierarchy
Created a robust exception hierarchy with a base `TimeZoneException` and three specific exception types:

- **`TimeZoneException`** (abstract base)
  - Provides common base for all timezone-related errors
  - Allows catching all timezone exceptions with a single catch block

- **`InvalidCityException`**
  - Thrown when city name is null, empty, or whitespace
  - Carries the `CityName` property for diagnostics
  - Used in validation scenarios

- **`InvalidTimeZoneIdException`**
  - Thrown when timezone ID is invalid
  - Carries `TimeZoneId` and optional `City` properties
  - Provides context about which city caused the issue

- **`CityNotFoundException`**
  - Thrown when a city doesn't exist in the mapping
  - Includes `CityName` and optional `AvailableCities` list
  - Helpful error messages that suggest available options

  - **`InvalidCitySortOrderException`**
  - Thrown when an invalid or unsupported city sort order is specified
  - Carries the `SortOrder` property for diagnostics
  - Used by the `GetAvailableCities` and `GetAvailableCityNames` methods
  - Helps ensure invalid enum values are caught early

### 2. Enhanced Interface
Updated `ITimeZoneProvider` with:
- `GetTimeZoneId(string city)` - Non-try version that throws exceptions
- `GetAvailableCities()` - Returns all mapped cities
- XML documentation with exception tags

### 3. Improved Implementation
`TimeZoneProvider` now:
- Uses custom exceptions instead of generic `ArgumentException`
- Implements `GetTimeZoneId()` with proper validation
- Provides `GetAvailableCities()` for better error messages
- Returns helpful error context (e.g., available cities when city not found)

### 4. Comprehensive Tests
Added 12 new tests covering:
- Exception throwing scenarios
- Property validation on exceptions
- Available cities functionality
- Edge cases (null, empty, whitespace)

## ?? Benefits

1. **Type Safety**
   ```csharp
   try {
       var tz = provider.GetTimeZoneId("Paris");
   }
   catch (CityNotFoundException ex) {
       Console.WriteLine($"City '{ex.CityName}' not found!");
       Console.WriteLine($"Try: {string.Join(", ", ex.AvailableCities)}");
   }
   ```

2. **Rich Diagnostics**
   - Exception properties carry relevant context
   - Error messages are automatically helpful
   - Easy to log and debug

3. **API Clarity**
   - Methods clearly document what exceptions they throw
   - Consumers know exactly what to catch
   - IntelliSense shows exception documentation

4. **Testability**
   - Easy to test for specific error conditions
   - Can assert on exception properties
   - Clear test intent

## ?? Files Created/Modified

### New Files:
- ? `Exceptions/TimeZoneException.cs`
- ? `Exceptions/InvalidCityException.cs`
- ? `Exceptions/InvalidTimeZoneIdException.cs`
- ? `Exceptions/CityNotFoundException.cs`
- ? `Exceptions/InvalidCitySortOrderException.cs`

### Modified Files:
- ? `ITimeZoneProvider.cs` - Added new methods and exception documentation
- ? `TimeZoneProvider.cs` - Implemented custom exceptions
- ? `TimeZoneProviderTests.cs` - Added comprehensive exception tests

## ?? Test Results
? **22 tests passed** - All tests green!

## ?? Usage Examples

### Try-Get Pattern (No exceptions)
```csharp
if (provider.TryGetTimeZoneId("London", out var tz)) {
    // Use tz
}
```

### Direct Get (With exceptions)
```csharp
try {
    var tz = provider.GetTimeZoneId(userInput);
    // Use tz
}
catch (InvalidCityException ex) {
    // Handle empty/null city
}
catch (CityNotFoundException ex) {
    // Suggest available cities
    Console.WriteLine($"Try: {string.Join(", ", ex.AvailableCities)}");
}
```

### Catch All Timezone Errors
```csharp
try {
    var tz = provider.GetTimeZoneId(city);
}
catch (TimeZoneException ex) {
    // Handle any timezone-related error
}
```

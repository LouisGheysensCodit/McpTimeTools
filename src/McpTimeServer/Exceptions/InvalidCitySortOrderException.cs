namespace McpTimeServer.Exceptions;

/// <summary>
/// Thrown when an invalid or unsupported city sort order is specified.
/// </summary>
public class InvalidCitySortOrderException : Exception
{
    public CitySortOrder SortOrder { get; }

    public InvalidCitySortOrderException(CitySortOrder sortOrder)
        : base($"Invalid sort order '{sortOrder}' specified.")
    {
        SortOrder = sortOrder;
    }
}

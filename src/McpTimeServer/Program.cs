using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using McpTimeServer;

var builder = Host.CreateEmptyApplicationBuilder(null);

// Register services
builder.Services.AddSingleton<ITimeZoneProvider, TimeZoneProvider>();
builder.Services.AddSingleton(TimeProvider.System);

// Add MCP server
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

var app = builder.Build();

Console.WriteLine("MCP Time Server is running...");

await app.RunAsync();
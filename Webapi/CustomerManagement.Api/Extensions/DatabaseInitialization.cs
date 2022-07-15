using CustomerManagement.Infrastructure.Database.Seed;

namespace CustomerManagement.Api.Extensions;

public static class DatabaseInitialization
{
    public static async Task InitializeDatabase(this WebApplication app)
    {
        using(var scope = app.Services.CreateScope())
        {
            var initializer = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();

            await initializer.Initialize();
        }
    }
}
namespace Simsa.Persistence;

using Microsoft.Extensions.DependencyInjection;

public static class DbContextInitializer
{
    public static void MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<SimsaDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}
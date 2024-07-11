namespace Simsa.Persistence;

using Microsoft.EntityFrameworkCore;

internal class SimsaDbContext : DbContext
{
    public SimsaDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
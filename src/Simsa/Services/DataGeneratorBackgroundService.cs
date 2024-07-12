namespace Simsa.Services;

using Simsa.DataGenerator;

public class DataGeneratorBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory serviceScopeFactory;

    public DataGeneratorBackgroundService(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var serviceScope = this.serviceScopeFactory.CreateScope();
        await new DataGenerator(serviceScope.ServiceProvider).PopulateDatabaseAsync(cancellationToken);
    }
}
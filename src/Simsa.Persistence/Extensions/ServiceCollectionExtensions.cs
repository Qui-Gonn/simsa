namespace Simsa.Persistence.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Simsa.Core.Repositories;
using Simsa.Model;
using Simsa.Persistence.Entities;
using Simsa.Persistence.Mapper;
using Simsa.Persistence.Repositories;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSimsaPersistenceServices(this IServiceCollection services, string connectionString)
    {
        services.AddSimsaDbContext(connectionString);

        services.Register<EventEntity, Event, EventMapper, IEventRepository, EventRepository>();
        services.Register<PersonEntity, Person, PersonMapper, IPersonRepository, PersonRepository>();

        return services;
    }

    private static void AddSimsaDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<SimsaDbContext>(
            options
                => options
                    .UseSqlite(connectionString)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
    }

    private static void Register<TEntity, TModel, TMapperImplementation, TRepositoryInterface, TRepositoryImplementation>(
        this IServiceCollection services)
        where TMapperImplementation : class, IMapper<TEntity, TModel>
        where TRepositoryInterface : class, IGenericRepository<TModel>
        where TRepositoryImplementation : class, TRepositoryInterface
    {
        services.AddScoped<IMapper<TEntity, TModel>, TMapperImplementation>();
        services.AddScoped<TRepositoryInterface, TRepositoryImplementation>();
        services.AddScoped<IGenericRepository<TModel>>(serviceProvider => serviceProvider.GetRequiredService<TRepositoryInterface>());
    }
}
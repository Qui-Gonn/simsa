namespace Tkd.Simsa.Persistence.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Application.EventManagement;
using Tkd.Simsa.Application.PersonManagement;
using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Domain.PersonManagement;
using Tkd.Simsa.Persistence.Entities;
using Tkd.Simsa.Persistence.Mapper;
using Tkd.Simsa.Persistence.Repositories;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSimsaPersistenceServices(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddSimsaDbContext(connectionString);

        services.Register<EventEntity, Event, EventMapper, IEventRepository, EventRepository>();
        services.Register<PersonEntity, Person, PersonMapper, IPersonRepository, PersonRepository>();
        services.Register<ExaminationResultEntity, ExaminationResult, ExaminationResultMapper, IExaminationResultRepository, ExaminationResultRepository>();
        services.Register<ExaminationEntity, Examination, ExaminationMapper, IExaminationRepository, ExaminationRepository>();

        return services;
    }

    private static void AddSimsaDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddPooledDbContextFactory<SimsaDbContext>((_, options) => options
                                                               .UseSqlite(connectionString)
                                                               // .UseSeeding((_, _) =>
                                                               //             {
                                                               //                 using var scope = sp.CreateScope();
                                                               //                 new DataGenerator(scope.ServiceProvider)
                                                               //                     .PopulateDatabaseAsync()
                                                               //                     .RunSynchronously();
                                                               //             })
                                                               // .UseAsyncSeeding(async (_, _, cancellationToken) =>
                                                               //                  {
                                                               //                      await using var scope = sp.CreateAsyncScope();
                                                               //                      await new DataGenerator(scope.ServiceProvider)
                                                               //                          .PopulateDatabaseAsync(cancellationToken);
                                                               //                  })
                                                               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        services.AddScoped(sp => sp.GetRequiredService<IDbContextFactory<SimsaDbContext>>().CreateDbContext());
    }

    private static void Register<TEntity, TModel, TMapperImplementation, TRepositoryInterface,
        TRepositoryImplementation>(
        this IServiceCollection services)
        where TMapperImplementation : class, IMapper<TEntity, TModel>
        where TRepositoryInterface : class, IGenericRepository<TModel>
        where TRepositoryImplementation : class, TRepositoryInterface
    {
        services.AddScoped<IMapper<TEntity, TModel>, TMapperImplementation>();
        services.AddScoped<TRepositoryInterface, TRepositoryImplementation>();
        services.AddScoped<IGenericRepository<TModel>>(sp => sp.GetRequiredService<TRepositoryInterface>());
    }
}
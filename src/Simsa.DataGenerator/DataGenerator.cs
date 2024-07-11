namespace Simsa.DataGenerator;

using Bogus;

using Microsoft.Extensions.DependencyInjection;

using Simsa.Core.Repositories;
using Simsa.Model;

public class DataGenerator(IServiceProvider serviceProvider)
{
    private const int Count = 25;

    private readonly Faker<Event> events = new Faker<Event>()
        .StrictMode(true)
        .RuleFor(i => i.Id, Guid.NewGuid)
        .RuleFor(i => i.Name, f => $"Exam {f.Random.Number(100)}")
        .RuleFor(i => i.Description, f => f.Lorem.Text())
        .RuleFor(
            i => i.StartDate,
            f => f.Date.BetweenDateOnly(
                DateOnly.FromDateTime(DateTime.UtcNow - TimeSpan.FromDays(365)),
                DateOnly.FromDateTime(DateTime.UtcNow + TimeSpan.FromDays(30))))
        .RuleFor(i => i.Participations, Participations.NoParticipations);

    private readonly Faker<Model.Person> persons = new Faker<Model.Person>()
        .StrictMode(true)
        .RuleFor(i => i.Id, Guid.NewGuid)
        .RuleFor(i => i.FirstName, f => f.Person.FirstName)
        .RuleFor(i => i.LastName, f => f.Person.LastName)
        .RuleFor(i => i.DateOfBirth, f => DateOnly.FromDateTime(f.Person.DateOfBirth))
        .RuleFor(i => i.Gender, f => f.PickRandom<Gender>());

    public async Task PopulateDatabaseAsync(CancellationToken cancellationToken = default)
    {
        await this.PopulateAsync(this.events, cancellationToken);
        await this.PopulateAsync(this.persons, cancellationToken);
    }

    private async Task PopulateAsync<TModel>(Faker<TModel> faker, CancellationToken cancellationToken = default)
        where TModel : class
    {
        faker.AssertConfigurationIsValid();
        var repository = serviceProvider.GetRequiredService<IGenericRepository<TModel>>();

        var generatedItems = faker.Generate(Count);
        foreach (var item in generatedItems)
        {
            await repository.AddAsync(item, false, cancellationToken);
        }

        await repository.SaveChangesAsync(cancellationToken);
    }
}
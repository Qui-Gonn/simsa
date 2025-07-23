namespace Tkd.Simsa.DataGenerator;

using Bogus;

using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Domain.PersonManagement;

internal class FakerCollection
{
    public readonly Faker<Domain.EventManagement.Event> EventFaker;

    public readonly Dictionary<IFakerTInternal, object> GeneratedItems = new ();

    public readonly Faker<Domain.PersonManagement.Person> PersonFaker;

    public FakerCollection()
    {
        this.PersonFaker = this.DefinePersonFaker();
        this.EventFaker = this.DefineEventFaker();
    }

    public List<T> GetItems<T>(Faker<T> faker)
        where T : class
    {
        return (List<T>)this.GeneratedItems[faker];
    }

    private Faker<Event> DefineEventFaker()
    {
        var faker = new Faker<Domain.EventManagement.Event>()
                    .StrictMode(true)
                    .RuleFor(i => i.Id, Guid.NewGuid)
                    .RuleFor(i => i.Description, f => f.Lorem.Text())
                    .RuleFor(i => i.Name, f => $"Exam {f.Random.Number(100)}")
                    .RuleFor(
                        i => i.ParticipationData,
                        f =>
                        {
                            var randomPersons = f.PickRandom(this.GetItems(this.PersonFaker), f.Random.Number(3, 8));
                            var participants = randomPersons.Select(p => new Participant
                                                            {
                                                                Id = Guid.CreateVersion7(),
                                                                PersonId = p.Id,
                                                                PersonInfo = p
                                                            })
                                                            .ToList();
                            return new ParticipationData(participants);
                        })
                    .RuleFor(
                        i => i.StartDate,
                        f => f.Date.BetweenDateOnly(
                            DateOnly.FromDateTime(DateTime.UtcNow - TimeSpan.FromDays(365)),
                            DateOnly.FromDateTime(DateTime.UtcNow + TimeSpan.FromDays(30))))
                    .RuleFor(i => i.ExaminationDisciplines, f => Enumerable.Range(1, 3).Select(_ => Discipline.Create(
                        f.PickRandom<DisciplineType>(),
                        f.Lorem.Word(),
                        f.Lorem.Sentence(),
                        f.Random.Int(1, 10))).ToList())
                    .RuleFor(i => i.ExaminationProgress, f => ExaminationProgress.CreateInitial(
                        Enumerable.Range(1, 3).Select(_ => Discipline.Create(
                            f.PickRandom<DisciplineType>(),
                            f.Lorem.Word(),
                            f.Lorem.Sentence(),
                            f.Random.Int(1, 10))).ToList()));

        var generatedItems = faker.Generate(25);
        this.GeneratedItems[faker] = generatedItems;
        return faker;
    }

    private Faker<Domain.PersonManagement.Person> DefinePersonFaker()
    {
        var faker = new Faker<Domain.PersonManagement.Person>()
                    .StrictMode(true)
                    .RuleFor(i => i.Id, Guid.NewGuid)
                    .RuleFor(i => i.Name, f => new PersonName(f.Person.FirstName, f.Person.LastName))
                    .RuleFor(i => i.DateOfBirth, f => BirthDate.FromDateTime(f.Person.DateOfBirth))
                    .RuleFor(i => i.Gender, f => f.PickRandom<Gender>());

        var generatedItems = faker.Generate(25);
        this.GeneratedItems[faker] = generatedItems;
        return faker;
    }
}
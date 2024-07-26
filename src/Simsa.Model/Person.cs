namespace Simsa.Model;

public class Person : IModel, IHasId<Guid>
{
    public BirthDate DateOfBirth { get; set; } = BirthDate.Empty;

    public Gender Gender { get; set; } = Gender.Unknown;

    public Guid Id { get; set; } = Guid.NewGuid();

    public PersonName Name { get; set; } = new (string.Empty, string.Empty);
}
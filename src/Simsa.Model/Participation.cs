namespace Simsa.Model;

public class Participation
{
    public DateOnly DateOfBirth { get; set; }

    public Guid EventId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public Gender Gender { get; set; } = Gender.Unknown;

    public long Id { get; set; }

    public string LastName { get; set; } = string.Empty;

    public Person Person { get; set; }

    public Rank Rank { get; set; }
}
namespace Simsa.Model;

public class Person
{
    public DateOnly DateOfBirth { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public Gender Gender { get; set; } = Gender.Unknown;

    public Guid Id { get; set; }

    public string LastName { get; set; } = string.Empty;
}
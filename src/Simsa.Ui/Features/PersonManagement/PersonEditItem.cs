namespace Simsa.Ui.Features.PersonManagement;

using Simsa.Model;

public class PersonEditItem
{
    public DateTime? DateOfBirth
    {
        get => this.Source.DateOfBirth != default ? this.Source.DateOfBirth.ToDateTime(TimeOnly.MinValue) : null;
        set => this.Source.DateOfBirth = DateOnly.FromDateTime(value ?? default);
    }

    public required Person Source { get; init; }

    public static PersonEditItem FromPerson(Person source)
        => new ()
        {
            Source = source
        };
}
namespace Simsa.Ui.Features.PersonManagement;

using Simsa.Model;

public class PersonEditItem
{
    private PersonEditItem(Person source)
    {
        this.Source = source;
    }

    public DateTime? DateOfBirth
    {
        get => this.Source.DateOfBirth.ToDateTime();
        set => this.Source.DateOfBirth = Model.BirthDate.FromDateTime(value ?? default);
    }

    public string FirstName
    {
        get => this.Source.Name.FirstName;
        set => this.Source.Name = this.Source.Name with { FirstName = value };
    }

    public Gender Gender
    {
        get => this.Source.Gender;
        set => this.Source.Gender = value;
    }

    public Guid Id => this.Source.Id;

    public string LastName
    {
        get => this.Source.Name.LastName;
        set => this.Source.Name = this.Source.Name with { LastName = value };
    }

    public Person Source { get; }

    public static PersonEditItem FromPerson(Person source)
        => new (source);
}
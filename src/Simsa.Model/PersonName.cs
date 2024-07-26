namespace Simsa.Model;

public record PersonName(string FirstName, string LastName)
{
    private const string FormatLastNameFirstName = "{0}, {1}";

    private string DisplayName { get; } = string.Format(FormatLastNameFirstName, LastName, FirstName);

    public override string ToString() => this.DisplayName;
}
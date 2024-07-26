namespace Simsa.Model;

public record BirthDate
{
    public static BirthDate Empty = FromDateOnly(DateOnly.MinValue);

    private BirthDate()
    {
    }

    public int Age => this.CalculateAge(DateTime.Today);

    public DateOnly Date { get; init; }

    public static BirthDate FromDateOnly(DateOnly birthDate)
        => new ()
        {
            Date = birthDate
        };

    public static BirthDate FromDateTime(DateTime birthDate)
        => new ()
        {
            Date = DateOnly.FromDateTime(birthDate)
        };

    public int CalculateAge(DateTime referenceDate)
        => this.CalculateAge(DateOnly.FromDateTime(referenceDate));

    public int CalculateAge(DateOnly referenceDate)
    {
        var age = referenceDate.Year - this.Date.Year;

        if (referenceDate.Month < this.Date.Month
            || (referenceDate.Month == this.Date.Month
                && referenceDate.Day < this.Date.Day))
        {
            age--;
        }

        return age;
    }

    public DateTime ToDateTime()
        => this.Date.ToDateTime(TimeOnly.MinValue);
}
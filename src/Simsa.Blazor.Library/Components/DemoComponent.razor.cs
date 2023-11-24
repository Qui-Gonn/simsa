namespace Simsa.Blazor.Library.Components;

using System.ComponentModel.DataAnnotations;

public partial class DemoComponent
{
    private Person Person => new ("Michael Binder", new DateOnly(1989, 2, 8), Gender.Male);
}

public class Person
{
    public Person(string name, DateOnly dateOfBirth, Gender gender)
    {
        this.Name = name;
        this.DateOfBirth = dateOfBirth;
        this.Gender = gender;
    }

    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public DateOnly DateOfBirth { get; set; }

    [Display(Name = "Gender")]
    public Gender Gender { get; set; }

    [Display(Name = "Name")]
    [Required]
    [StringLength(255, MinimumLength = 3)]
    public string Name { get; set; }
}

public enum Gender
{
    Unknown = 0,

    Female = 1,

    Male = 2,

    Other = 3
}
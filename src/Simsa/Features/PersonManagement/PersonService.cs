namespace Simsa.Features.PersonManagement;

using Simsa.Interfaces.Features.PersonManagement;
using Simsa.Model;

public class PersonService : GenericItemService<Person>, IPersonService
{
    static PersonService()
    {
        var date1 = DateOnly.Parse("1960-1-11");
        var date2 = DateOnly.Parse("1974-7-26");
        var date3 = DateOnly.Parse("1952-1-30");

        Items =
        [
            new Person
            {
                DateOfBirth = date1,
                FirstName = "Thorsten",
                Gender = Gender.Male,
                Id = Guid.NewGuid(),
                LastName = "Lange",
            },
            new Person
            {
                DateOfBirth = date2,
                FirstName = "Marie",
                Gender = Gender.Female,
                Id = Guid.NewGuid(),
                LastName = "Huber",
            },
            new Person
            {
                DateOfBirth = date3,
                FirstName = "Jörg",
                Gender = Gender.Male,
                Id = Guid.NewGuid(),
                LastName = "Friedmann",
            }
        ];
    }
}
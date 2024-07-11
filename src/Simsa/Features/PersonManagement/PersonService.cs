namespace Simsa.Features.PersonManagement;

using Simsa.Interfaces.Features.PersonManagement;
using Simsa.Interfaces.Repositories;
using Simsa.Model;

public class PersonService : GenericItemService<Person>, IPersonService
{
    public PersonService(IGenericRepository<Person> repository)
        : base(repository)
    {
    }
}
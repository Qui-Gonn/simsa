namespace Simsa.Features.PersonManagement;

using Simsa.Core.Features.PersonManagement;
using Simsa.Core.Repositories;
using Simsa.Model;

public class PersonService : GenericItemService<Person>, IPersonService
{
    public PersonService(IGenericRepository<Person> repository)
        : base(repository)
    {
    }
}
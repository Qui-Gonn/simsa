namespace Simsa.Wasm.Features.PersonManagement;

using Simsa.Interfaces.Features.PersonManagement;
using Simsa.Model;

public class PersonService : GenericItemService<Person>, IPersonService
{
    private const string Endpoint = "api/persons";

    public PersonService(ApiHttpClient apiClient)
        : base(apiClient, Endpoint)
    {
    }
}
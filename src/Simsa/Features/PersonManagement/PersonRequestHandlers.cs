namespace Simsa.Features.PersonManagement;

using Simsa.Core.Repositories;
using Simsa.Model;

public class GetAllPersonsHandler(IPersonRepository repository) : GetAllItemsHandler<Person>(repository);

public class GetPersonByIdHandler(IPersonRepository repository) : GetItemByIdHandler<Person>(repository);

public class AddPersonHandler(IPersonRepository repository) : AddItemHandler<Person>(repository);

public class UpdatePersonHandler(IPersonRepository repository) : UpdateItemHandler<Person>(repository);

public class DeletePersonHandler(IPersonRepository repository) : DeleteItemHandler<Person>(repository);
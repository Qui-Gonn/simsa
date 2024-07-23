namespace Simsa.Features.PersonManagement;

using Simsa.Core.Repositories;
using Simsa.Model;

internal class GetAllPersonsHandler(IPersonRepository repository) : GetAllItemsHandler<Person>(repository);

internal class GetPersonByIdHandler(IPersonRepository repository) : GetItemByIdHandler<Person>(repository);

internal class AddPersonHandler(IPersonRepository repository) : AddItemHandler<Person>(repository);

internal class UpdatePersonHandler(IPersonRepository repository) : UpdateItemHandler<Person>(repository);

internal class DeletePersonHandler(IPersonRepository repository) : DeleteItemHandler<Person>(repository);
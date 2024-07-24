namespace Simsa.Wasm.Features.PersonManagement;

using Simsa.Core.Services;
using Simsa.Model;

internal class GetAllPersonsHandler(IGenericItemService<Person> service) : GetAllItemsHandler<Person>(service);

internal class GetPersonByIdHandler(IGenericItemService<Person> service) : GetItemByIdHandler<Person>(service);

internal class AddPersonHandler(IGenericItemService<Person> service) : AddItemHandler<Person>(service);

internal class UpdatePersonHandler(IGenericItemService<Person> service) : UpdateItemHandler<Person>(service);

internal class DeletePersonHandler(IGenericItemService<Person> service) : DeleteItemHandler<Person>(service);
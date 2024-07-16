namespace Simsa.Wasm.Features.PersonManagement;

using Simsa.Core.Services;
using Simsa.Features;
using Simsa.Model;

public class GetAllPersonsHandler(IGenericItemService<Person> service) : GetAllItemsHandler<Person>(service);

public class GetPersonByIdHandler(IGenericItemService<Person> service) : GetItemByIdHandler<Person>(service);

public class AddPersonHandler(IGenericItemService<Person> service) : AddItemHandler<Person>(service);

public class UpdatePersonHandler(IGenericItemService<Person> service) : UpdateItemHandler<Person>(service);

public class DeletePersonHandler(IGenericItemService<Person> service) : DeleteItemHandler<Person>(service);
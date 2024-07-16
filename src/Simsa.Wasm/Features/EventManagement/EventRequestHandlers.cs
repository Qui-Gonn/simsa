namespace Simsa.Wasm.Features.EventManagement;

using Simsa.Core.Services;
using Simsa.Features;
using Simsa.Model;

public class GetAllEventsHandler(IGenericItemService<Event> service) : GetAllItemsHandler<Event>(service);

public class GetEventByIdHandler(IGenericItemService<Event> service) : GetItemByIdHandler<Event>(service);

public class AddEventHandler(IGenericItemService<Event> service) : AddItemHandler<Event>(service);

public class UpdateEventHandler(IGenericItemService<Event> service) : UpdateItemHandler<Event>(service);

public class DeleteEventHandler(IGenericItemService<Event> service) : DeleteItemHandler<Event>(service);
namespace Simsa.Wasm.Features.EventManagement;

using Simsa.Core.Services;
using Simsa.Model;

internal class GetAllEventsHandler(IGenericItemService<Event> service) : GetAllItemsHandler<Event>(service);

internal class GetEventByIdHandler(IGenericItemService<Event> service) : GetItemByIdHandler<Event>(service);

internal class AddEventHandler(IGenericItemService<Event> service) : AddItemHandler<Event>(service);

internal class UpdateEventHandler(IGenericItemService<Event> service) : UpdateItemHandler<Event>(service);

internal class DeleteEventHandler(IGenericItemService<Event> service) : DeleteItemHandler<Event>(service);
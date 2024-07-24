namespace Simsa.Features.EventManagement;

using Simsa.Core.Repositories;
using Simsa.Model;

internal class GetAllEventsHandler(IEventRepository repository) : GetAllItemsHandler<Event>(repository);

internal class GetEventByIdHandler(IEventRepository repository) : GetItemByIdHandler<Event>(repository);

internal class AddEventHandler(IEventRepository repository) : AddItemHandler<Event>(repository);

internal class UpdateEventHandler(IEventRepository repository) : UpdateItemHandler<Event>(repository);

internal class DeleteEventHandler(IEventRepository repository) : DeleteItemHandler<Event>(repository);
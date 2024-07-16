namespace Simsa.Features.EventManagement;

using Simsa.Core.Repositories;
using Simsa.Model;

public class GetAllEventsHandler(IEventRepository repository) : GetAllItemsHandler<Event>(repository);

public class GetEventByIdHandler(IEventRepository repository) : GetItemByIdHandler<Event>(repository);

public class AddEventHandler(IEventRepository repository) : AddItemHandler<Event>(repository);

public class UpdateEventHandler(IEventRepository repository) : UpdateItemHandler<Event>(repository);

public class DeleteEventHandler(IEventRepository repository) : DeleteItemHandler<Event>(repository);
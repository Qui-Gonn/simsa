namespace Simsa.Features.EventManagement;

using Simsa.Model;

public interface IEventService
{
    ValueTask<Event[]> GetAll();
}
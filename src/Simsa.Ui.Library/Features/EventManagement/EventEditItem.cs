namespace Simsa.Ui.Library.Features.EventManagement;

using Simsa.Model;

public class EventEditItem
{
    public EventEditItem(Event source)
    {
        this.Source = source;
    }

    public Event Source { get; }

    public DateTime? StartDate
    {
        get => this.Source.StartDate.ToDateTime(TimeOnly.MinValue);
        set => this.Source.StartDate = DateOnly.FromDateTime(value ?? default);
    }

    public static EventEditItem FromEvent(Event source) => new (source);
}
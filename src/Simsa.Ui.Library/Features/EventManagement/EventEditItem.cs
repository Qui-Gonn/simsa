namespace Simsa.Ui.Library.Features.EventManagement;

using Simsa.Model;

public class EventEditItem
{
    public required Event Source { get; init; }

    public DateTime? StartDate
    {
        get => this.Source.StartDate.ToDateTime(TimeOnly.MinValue);
        set => this.Source.StartDate = DateOnly.FromDateTime(value ?? default);
    }

    public static EventEditItem FromEvent(Event source)
        => new ()
        {
            Source = source
        };
}
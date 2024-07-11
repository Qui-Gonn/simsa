namespace Simsa.Persistence.Entities;

using Simsa.Model;

internal class EventEntity : IHasId<Guid>
{
    public string? Description { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }
}
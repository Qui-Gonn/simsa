namespace Simsa.Model;

public class Event : IModel, IHasId<Guid>
{
    public string Description { get; set; } = string.Empty;

    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public Participations Participations { get; set; } = Participations.NoParticipations;

    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
}
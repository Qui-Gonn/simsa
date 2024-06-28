﻿namespace Simsa.Model;

public class Event
{
    public string Description { get; set; } = string.Empty;

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Participations Participations { get; set; } = Participations.NoParticipations;

    public DateOnly StartDate { get; set; }
}
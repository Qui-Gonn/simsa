namespace Simsa.Model;

public class Participations
{
    public static readonly Participations NoParticipations = new ();

    public List<Participation> Data { get; set; } = [];
}
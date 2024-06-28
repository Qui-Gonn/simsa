namespace Simsa.Model;

public struct RankType
{
    public static readonly RankType Dan = new ("Dan", "Degree");

    public static readonly RankType Kup = new ("Kup", "Grade");

    public static readonly RankType None = new ("None", "None");

    private RankType(string name, string altName)
    {
        this.Name = name;
        this.AltName = altName;
    }

    public string AltName { get; } = string.Empty;

    public string Name { get; } = string.Empty;
}
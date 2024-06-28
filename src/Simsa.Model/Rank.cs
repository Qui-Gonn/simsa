namespace Simsa.Model;

public abstract class Rank
{
    protected Rank(int id, int nr, RankType rankType)
    {
        this.Id = id;
        this.Nr = nr;
        this.RankType = rankType;
    }

    public int Id { get; }

    public int Nr { get; }

    public RankType RankType { get; }
}
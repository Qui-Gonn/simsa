namespace Simsa.Model;

public class DanRank : Rank
{
    public static readonly DanRank EighthDan = new (8);

    public static readonly DanRank FifthDan = new (5);

    public static readonly DanRank FirstDan = new (1);

    public static readonly DanRank FourthDan = new (4);

    public static readonly DanRank NinthDan = new (9);

    public static readonly DanRank SecondDan = new (2);

    public static readonly DanRank SeventhDan = new (7);

    public static readonly DanRank SixthDan = new (6);

    public static readonly DanRank ThirdDan = new (3);

    private DanRank(int nr)
        : base(nr + 1000, nr, RankType.Dan)
    {
    }
}
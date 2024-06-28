namespace Simsa.Model;

public class KupRank : Rank
{
    public static readonly KupRank EighthKup = new (8);

    public static readonly KupRank FifthKup = new (5);

    public static readonly KupRank FirstKup = new (1);

    public static readonly KupRank FourthKup = new (4);

    public static readonly KupRank NinthKup = new (9);

    public static readonly KupRank SecondKup = new (2);

    public static readonly KupRank SeventhKup = new (7);

    public static readonly KupRank SixthKup = new (6);

    public static readonly KupRank TenthKup = new (10);

    public static readonly KupRank ThirdKup = new (3);

    private KupRank(int nr)
        : base(nr + 100, nr, RankType.Kup)
    {
    }
}
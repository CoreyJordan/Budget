using DropsLibrary;

namespace DropsUI;

/// <summary>
/// Testing class to populate data for building.
/// All data will be generated from database in final.
/// </summary>
public class SampleData
{
    private Bucket electricBill = new()
    {
        Name = "Electric Bill",
        Category = Category.Utilities,
        Budget = 150M,
        Drops = 150M
    };

    private Bucket internet = new()
    {
        Name = "Internet",
        Category = Category.Utilities,
        Budget = 115.27M,
        Drops = 0M
    };

    private Bucket netflix = new()
    {
        Name = "Netflix",
        Category = Category.Subscription,
        Budget = 15M,
        Drops = 10M
    };

    private Bucket hulu = new()
    {
        Name = "Hulu",
        Category = Category.Subscription,
        Budget = 8.55M,
        Drops = 7M
    };

    private Bucket peacock = new()
    {
        Name = "Peacock",
        Category = Category.Subscription,
        Budget = 3.99M,
        Drops = 0M
    };

    public List<Bucket> Buckets { get; set; }

    public SampleData()
    {
        Buckets = new()
        {
            electricBill,
            internet,
            netflix,
            hulu,
            peacock
        };
    }
}
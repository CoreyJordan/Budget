using System.Data.SqlTypes;

namespace DropsLibrary;

/// <summary>
/// A container for tracking Drops, goals, and progress towards them.
/// </summary>
public class Bucket
{
    // Properties

    /// <summary>
    /// Represents the user generated identification of the bucket.
    /// </summary>
    public string? Name { get; set; } = string.Empty;

    /// <summary>
    /// Represents the financial target being tracked by the bucket.
    /// </summary>
    public Category Category { get; set; } = Category.Misc;

    /// <summary>
    /// Represents the user set target funding for this bucket.
    /// </summary>
    public decimal Budget { get; set; } = 0M;

    /// <summary>
    /// Represents the goal date. User can set date or date should be set
    /// to end of set period (month, week, etc)
    /// </summary>
    public SqlDateTime? FillDate { get; set; } = new();

    /// <summary>
    /// Represents the current amount of funding allocated towards filling
    /// this bucket.
    /// </summary>
    public decimal Drops { get; set; } = 0M;

    /// <summary>
    /// The transaction history (debits, deposits, transfers) of the bucket.
    /// </summary>
    public History History { get; set; } = new();

    /// <summary>
    /// Calculates and returns the current fill level (progress) of the bucket.
    /// </summary>
    public decimal Progress
    {
        get
        {
            return Drops / Budget;
        }
    }

    /// <summary>
    /// Represents the available funding after expenditures from the alloacted funds.
    /// </summary>
    public decimal WaterLevel
    {
        get
        {
            return Drops - History.DebitsAmount;
        }
    }

    // Constructors
    public Bucket()
    {
    }
}
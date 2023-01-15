namespace DropsLibrary;

public class History
{
    /// <summary>
    /// Represents a list of financial interactions regarding a bucket/account.
    /// </summary>
    public List<Transaction> TransactionHistory { get; set; } = new();
}
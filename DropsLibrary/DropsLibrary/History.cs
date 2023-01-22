namespace DropsLibrary;

public class History
{
    /// <summary>
    /// Represents a list of financial interactions regarding a bucket/account.
    /// </summary>
    public List<Transaction> TransactionHistory { get; set; } = new();

    /// <summary>
    /// Represents the total spending amount in the transaction history.
    /// </summary>
    public decimal DebitsAmount
    {
        get
        {
            decimal total = 0M;
            foreach (var transaction in TransactionHistory)
            {
                if (transaction.Type == TransactionType.Debit)
                {
                    total += transaction.Amount;
                }
            }
            return total;
        }
    }

    /// <summary>
    /// Represents the total deposits amount in the transaction history.
    /// </summary>
    public decimal CreditsAmount
    {
        get
        {
            decimal total = 0M;
            foreach (var transaction in TransactionHistory)
            {
                if (transaction.Type == TransactionType.Credit)
                {
                    total += transaction.Amount;
                }
            }
            return total;
        }
    }
}
using System.Data.SqlTypes;

namespace DropsLibrary;

public class Transaction
{
    // Properties

    /// <summary>
    /// Unique identifier of the the transaction.
    /// </summary>
    public int TransactionId { get; set; }

    /// <summary>
    /// The account from/to which the transaction is processed.
    /// </summary>
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// Represents the date the transaction was initiated. *Note: may not
    /// match date of actual execution.
    /// </summary>
    public SqlDateTime Date { get; set; } = new();

    /// <summary>
    /// Represents the account balance before the transaction is initiated.
    /// </summary>
    public decimal PreBalance { get; set; }

    /// <summary>
    /// Represents the amount of the transaction being processed.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Represents the balance once the transaction processes.
    /// </summary>
    public decimal PostBalance
    {
        get
        {
            return PreBalance + Amount;
        }
    }

    /// <summary>
    /// Represents the other end of the finanical transaction.
    /// </summary>
    public string Payee { get; set; } = string.Empty;

    /// <summary>
    /// User created notes about the transaction.
    /// </summary>
    public string Note { get; set; } = string.Empty;

    /// <summary>
    /// Classifies the transaction for handling.
    /// </summary>
    public TransactionType Type
    {
        get
        {
            return Amount switch
            {
                > 0 => TransactionType.Debit,
                < 0 => TransactionType.Credit,
                _ => TransactionType.None
            };
        }
    }
}
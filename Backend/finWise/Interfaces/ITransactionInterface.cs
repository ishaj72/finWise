using finWise.Model;

namespace finWise.Interfaces
{
    public interface ITransactionInterface
    {
        Task<TransactionDetails> NewTransactionAsync(TransactionDetails transactions);
        Task<bool> DeleteTransactionAsync(string transactionId);

        Task<TransactionDetails> GetTransactionByIdAsync(string transactionId);
        Task<IEnumerable<TransactionDetails>> GetTransactionsByUserIdAsync(string userId);

        // Task<TransactionDetails> UpdateTransactionAsync(string transactionId, TransactionDetails updatedTransaction);
    }
}

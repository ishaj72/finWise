using finWise.Model;

namespace finWise.Interfaces
{
    public interface IBudgetInterface
    {
        Task<BudgetDetails> NewBudgetAsync(BudgetDetails Budget);
        Task<BudgetDetails> UpdateBudgetAsync(string BudgetId, BudgetDetails budget);
        Task<bool> DeleteBudgetAsync(string BudgetId);
        Task<IEnumerable<BudgetDetails>> GetBudgetByUserIdAsync(string UserId);
        Task<BudgetDetails> GetBudgetByBudgetId(string BudgetId);

    }
}

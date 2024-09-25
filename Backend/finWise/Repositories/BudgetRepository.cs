using finWise.Interfaces;
using finWise.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finWise.Repositories
{
    public class BudgetRepository : IBudgetInterface
    {
        private readonly finWiseDbContext _context;
        public BudgetRepository(finWiseDbContext context)
        {
            _context = context;
        }
        private async Task<string> GenerateBudgetIdAsync()
        {
            var lastBudget = await _context.Budgets
                .OrderByDescending(b => b.BudgetID)
                .FirstOrDefaultAsync();

            if (lastBudget == null)
            {
                return "BUDGET100001";
            }

            var lastBudgetIdNumber = int.Parse(lastBudget.BudgetID.Substring(6));
            var newBudgetIdNumber = lastBudgetIdNumber + 1;

            return $"BUDGET{newBudgetIdNumber:D6}";
        }
        public async Task<BudgetDetails> NewBudgetAsync(BudgetDetails budget)
        {
            budget.BudgetID = await GenerateBudgetIdAsync();

            var newBudget = _context.Budgets.FindAsync(budget.BudgetID);
            if (newBudget != null)
            {
                throw new Exception("Budget with same id already exists");
            }
            _context.Budgets.AddAsync(budget);
            await _context.SaveChangesAsync();
            return budget;
        }
        public async Task<BudgetDetails> GetBudgetByBudgetId(string budgetId)
        {
            return await _context.Budgets.FirstOrDefaultAsync(b => b.BudgetID == budgetId);
        }
        public async Task<IEnumerable<BudgetDetails>> GetBudgetByUserIdAsync(string userId)
        {
            return await _context.Budgets.Where(b => b.UserId == userId).Include(b => b.User).ToListAsync();
        }
        public async Task<BudgetDetails> UpdateBudgetAsync(string budgetId, [FromQuery] BudgetDetails updateBudget)
        {
            var existBudget = await _context.Budgets.SingleOrDefaultAsync(b => b.BudgetID == budgetId);
            if (existBudget != null)
            {
                existBudget.StartDate = updateBudget.StartDate;
                existBudget.EndDate = updateBudget.EndDate;
                existBudget.BudgetAmount = updateBudget.BudgetAmount;
                await _context.SaveChangesAsync();

                return existBudget;
            }
            return null;
        }
        public async Task<bool> DeleteBudgetAsync(string budgetId)
        {
            var budgetToDelete = await _context.Budgets.FirstOrDefaultAsync(t => t.BudgetID == budgetId);

            if (budgetToDelete != null)
            {
                _context.Budgets.Remove(budgetToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}

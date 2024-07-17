using finWise.Interfaces;
using finWise.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finWise.Repositories
{
    public class TransactionRepository : ITransactionInterface
    {
        private readonly finWiseDbContext _context;

        public TransactionRepository(finWiseDbContext context)
        {
            _context = context;
        }

        private async Task<string> GenerateTransactionIdAsync()
        {
            var lastTransaction = await _context.Transactions
                .OrderByDescending(t => t.TransactionId)
                .FirstOrDefaultAsync();

            if (lastTransaction == null)
            {
                return "TRANSACT000001";
            }

            var lastTransactionIdNumber = int.Parse(lastTransaction.TransactionId.Substring(8));
            var newTransactionIdNumber = lastTransactionIdNumber + 1;

            return $"TRANSACT{newTransactionIdNumber:D6}";
        }

        public async Task<TransactionDetails> NewTransactionAsync(TransactionDetails transaction)
        {
            transaction.TransactionId = await GenerateTransactionIdAsync();

            var user = await _context.Users.FindAsync(transaction.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            transaction.User = user;
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }
        public async Task<bool> DeleteTransactionAsync(string transactionId)
        {
            var transactToDelete = await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == transactionId);

            if (transactToDelete != null)
            {
                _context.Transactions.Remove(transactToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<TransactionDetails> GetTransactionByIdAsync(string transactionId)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == transactionId);

        }
        public async Task<IEnumerable<TransactionDetails>> GetTransactionsByUserIdAsync(string userId)
        {
            return await _context.Transactions.Where(t => t.UserId == userId).Include(t => t.User).ToListAsync();
        }
        public async Task<TransactionDetails> UpdateTransactionAsync(string transactionId, [FromQuery] TransactionDetails updatedTransaction)
        {
            var existTransact = await _context.Transactions.SingleOrDefaultAsync(t => t.TransactionId == transactionId);
            if (existTransact != null)
            {
                existTransact.TransactionType = updatedTransaction.TransactionType;
                existTransact.Amount = updatedTransaction.Amount;
                existTransact.Date = updatedTransaction.Date;
                existTransact.Description = updatedTransaction.Description;
                await _context.SaveChangesAsync();

                return existTransact;
            }
            return null;
        }
    }
}

using finWise.Interfaces;
using finWise.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace finWise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionInterface _transactInterface;
        private readonly IConfiguration _configuration;

        public TransactionController(ITransactionInterface transactInterface, IConfiguration configuration)
        {
            _transactInterface = transactInterface;
            _configuration = configuration;
        }

        [Authorize(Roles = "User")]
        [HttpPost("NewTransaction")]
        public async Task<IActionResult> NewTransactionAsync([FromBody] TransactionDetails transact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var newTransact = await _transactInterface.NewTransactionAsync(transact);
                if (newTransact != null)
                {
                    return Ok(new { Message = "New Transaction added to the account", Transaction = newTransact });
                }
                return BadRequest(new { Message = "Error adding new transaction" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize(Roles = "User")]
        [HttpDelete("DeleteTransaction")]
        public async Task<IActionResult> DeleteTransactionAsync(string transactionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var deleted = await _transactInterface.DeleteTransactionAsync(transactionId);
                if (deleted)
                {
                    return Ok("Transaction deleted from the account");
                }
                return NotFound("Transaction not found");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet("GetTransactionById")]
        public async Task<IActionResult> GetTransactionByIdAsync(string transactionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var transactbyId = await _transactInterface.GetTransactionByIdAsync(transactionId);
            if (transactbyId != null)
            {
                return Ok(transactbyId);
            }
            return NotFound("Transaction Id not found");
        }

        [Authorize(Roles = "User")]
        [HttpGet("GetTransactionsByUserId")]
        public async Task<IActionResult> GetTransactionsByUserIdAsync(string userId)
        {
            try
            {
                var transactions = await _transactInterface.GetTransactionsByUserIdAsync(userId);
                if (transactions == null || !transactions.Any())
                {
                    return NotFound(new { Message = "No transactions found for this user" });
                }
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize(Roles = "User")]
        [HttpPut("UpdateTransaction")]
        public async Task<IActionResult> UpdateTransactionAsync(string transactionId, [FromBody] TransactionDetails updatedTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedTransact = await _transactInterface.UpdateTransactionAsync(transactionId, updatedTransaction);
                if (updatedTransact != null)
                {
                    return Ok(new { Message = "Transaction updated successfully", Transaction = updatedTransact });
                }
                return NotFound(new { Message = "Transaction not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

    }
}

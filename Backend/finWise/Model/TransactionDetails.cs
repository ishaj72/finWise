using System.ComponentModel.DataAnnotations;

namespace finWise.Model
{
    public class TransactionDetails
    {
        [Required]
        public string TransactionId { get; set; }
        [Required]
        public string TransactionType { get; set; } // groceries, utilties
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string Date { get; set; }
        [Required]
        public string Description { get; set; } // details regarding transaction
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finWise.Model
{
    public class TransactionDetails
    {
        [Key]
        public string TransactionId { get; set; }

        [Required]
        public string TransactionType { get; set; } // groceries, utilities

        [Required]
        public double Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string Category { get; set; } // details regarding transaction

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public UserDetails User { get; set; }
    }
}

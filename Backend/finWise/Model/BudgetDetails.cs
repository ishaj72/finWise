using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finWise.Model
{
    public class BudgetDetails
    {
        [Key]
        public string BudgetID { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public string StartDate { get; set; }

        [Required]
        public string EndDate { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public UserDetails User { get; set; }
    }
}

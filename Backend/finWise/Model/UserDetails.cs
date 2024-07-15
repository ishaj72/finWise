using System.ComponentModel.DataAnnotations;

namespace finWise.Model
{
    public class UserDetails
    {
        [Key]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid contact details")]
        public string PhoneNumber { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
        public string UserEmail { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Passwords credentials do not match")]
        public string Password { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Role { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FreelanceFrontend.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Email Is not Valid")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\\s).{8,15}$", ErrorMessage = "Password must contain one Uppercase, Lowercase,Numeric and Special Symbol")]
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceDAL.Models
{
    public class UserRegisterationModel
    {
        [Required(ErrorMessage = "Name Field is Required")]
        [MinLength(3)]
        [MaxLength(15)]
        [RegularExpression("^[a-zA-z]+([\\s][a-zA-Z]+)*$", ErrorMessage = "Name Field Contain Only Letter with Space")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Email Is not Valid")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\\s).{8,15}$", ErrorMessage = "Password must contain one Uppercase, Lowercase,Numeric and Special Symbol")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\\s).{8,15}$", ErrorMessage = "Password must contain one Uppercase, Lowercase,Numeric and Special Symbol")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public bool RegisterAsServiceProvider { get; set; }
    }
}

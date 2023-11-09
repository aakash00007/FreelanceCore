using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FreelanceFrontend.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Name { get; set; }
        public virtual ICollection<Service>? Service { get; set; }
    }
}

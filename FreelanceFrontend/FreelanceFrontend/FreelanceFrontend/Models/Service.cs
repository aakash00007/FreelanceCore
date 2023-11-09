using System.ComponentModel.DataAnnotations;

namespace FreelanceFrontend.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string? ImageUrl { get; set; }

        public string? UserId { get; set; }
        public virtual AppUser? User { get; set; }
        public int? ServiceTypeId { get; set; }
        public virtual ServiceType? ServiceType { get; set; }
    }
}

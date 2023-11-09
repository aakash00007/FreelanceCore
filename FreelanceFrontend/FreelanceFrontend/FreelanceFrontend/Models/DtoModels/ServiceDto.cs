using System.ComponentModel.DataAnnotations;

namespace FreelanceFrontend.Models.DtoModels
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string? ImageUrl { get; set; }

        public string? UserId { get; set; }
        public int? ServiceTypeId { get; set; }
    }
}

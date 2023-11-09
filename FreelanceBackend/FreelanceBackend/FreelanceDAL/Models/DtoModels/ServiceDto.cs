using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceDAL.Models.DtoModels
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public int? ServiceTypeId { get; set; }
        public string? UserId { get; set; }
    }
}

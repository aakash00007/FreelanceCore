using System.ComponentModel.DataAnnotations;

namespace FreelanceFrontend.Models.DtoModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Subject Field is Required")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Body Field is Required")]
        public string Body { get; set; }
        public string EmailTo { get; set; }
    }
}

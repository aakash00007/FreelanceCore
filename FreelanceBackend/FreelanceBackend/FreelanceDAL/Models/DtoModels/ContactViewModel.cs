using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceDAL.Models.DtoModels
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

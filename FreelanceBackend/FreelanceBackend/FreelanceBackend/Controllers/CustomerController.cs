using AutoMapper;
using EmailService;
using FreelanceDAL.Models.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FreelanceBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Bearer",Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        private IEmailSender _emailSender;
        private IMapper _mapper;

        public CustomerController(IEmailSender emailSender, IMapper mapper)
        {
            _emailSender = emailSender;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> ContactProvider(ContactViewModel contactViewModel)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Message mailMessage = new Message(contactViewModel.EmailTo, contactViewModel.Subject, contactViewModel.Body);
                    string fromEmail = User.FindFirstValue(ClaimTypes.Email);
                    await _emailSender.SendEmail(mailMessage, fromEmail); 
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }
                return Ok("Mail Sent Successfully!");
            }
            return BadRequest();
        }
    }
}

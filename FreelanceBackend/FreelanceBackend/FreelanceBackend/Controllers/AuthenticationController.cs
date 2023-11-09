using AutoMapper;
using EmailService;
using FreelanceDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FreelanceBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager; 
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;

        public AuthenticationController(IConfiguration config, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSender emailSender)
        {
            _config = config;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterationModel signupModel)
        {
            try
            {
                var userexist = await _userManager.FindByEmailAsync(signupModel.Email);
                if(userexist != null) 
                {
                    return StatusCode(409, "User Already Exists!");
                }
                var user = _mapper.Map<AppUser>(signupModel);
                var result = await _userManager.CreateAsync(user,signupModel.Password);
                if (result.Succeeded && signupModel.RegisterAsServiceProvider == true)
                {
                    await _userManager.AddToRoleAsync(user, "ServiceProvider");
                    return StatusCode(201, "User Registered Successfully!");
                }
                else if(result.Succeeded && signupModel.RegisterAsServiceProvider == false)
                {
                    await _userManager.AddToRoleAsync(user, "Customer");
                    return StatusCode(201, "User registered Successfully!!");
                }
                else
                {
                    return StatusCode(409, "User Already Exists!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(loginModel.Email);

                    if (user != null)
                    {
                        var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, lockoutOnFailure: false);

                        if (!result.Succeeded)
                        {
                            return Unauthorized();
                        }
                        
                        return Ok(new { token = GenerateToken(user) });
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok("Logged out Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
                    if (user == null)
                    {
                        return NotFound("No User found with the provided Email.");
                    }
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callback = Url.Action("ResetPassword", "Authentication", new { token, email = forgotPasswordModel.Email }, "http", "localhost:5219");
                    callback = callback.Replace("/api", "");
                    Message message = new Message(user.Email, "Reset Password Token", callback);
                    await _emailSender.SendEmail(message);
                    return Ok("Mail Send Successfully " + token);
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
                    if(user!= null)
                    {
                        var result = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
                        if (result.Succeeded)
                        {
                            return Ok("Password reset Successfully");
                        }
                        return BadRequest();
                    }
                    return NotFound("User Doesn't Exist!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }
            return BadRequest();
        }

        private string GenerateToken(AppUser user)
        {
            List<string> roles = _userManager.GetRolesAsync(user).Result.ToList();
            var claims = new[] {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, roles[0]),
            new Claim(ClaimTypes.NameIdentifier,
            user.Id)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                                             _config["Jwt:Audience"], 
                                             claims,
                                             expires: DateTime.Now.AddDays(1), 
                                             signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}

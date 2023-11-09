using FreelanceFrontend.Models;
using FreelanceFrontend.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FreelanceFrontend.Controllers
{
    public class AuthenticationController : Controller
    {
        private HttpClient _httpClient;
        private IClaimRepository _claimRepository;
        Uri baseAddress = new Uri("http://localhost:7241/api");
        public AuthenticationController(HttpClient httpClient,IClaimRepository claimRepository)
        {
            _httpClient = httpClient;
            _claimRepository = claimRepository;
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterationModel signupModel)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                string data = JsonConvert.SerializeObject(signupModel);
                StringContent content = new StringContent(data,Encoding.UTF8,"application/json");
                HttpResponseMessage response = await _httpClient.PostAsync( baseAddress + "/Authentication/SignUp", content);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    TempData["Success"] = "User Registered Successfully";
                    return RedirectToAction("Login");
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Internal Server error. Please try later!!");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel loginModel)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                string data = JsonConvert.SerializeObject(loginModel);
                StringContent content = new StringContent(data, Encoding.UTF8,"application/json");
                HttpResponseMessage response = await _httpClient.PostAsync( baseAddress + "/Authentication/Login", content);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    TokenConfig accessToken = JsonConvert.DeserializeObject<TokenConfig>(token);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(accessToken.Token);
                    var claimsIdentity = new ClaimsIdentity(securityToken.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                    string role = securityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;


                    //var accessToken = JsonConvert.DeserializeObject<TokenConfig>(token);
                    HttpContext.Session.SetString("AccessToken", accessToken.Token);
                    //var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
                    //var claimValue = _claimRepository.GetClaims(accessTokenFromSession).FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                    TempData["Success"] = "User SignedIn Successfully";
                    if (role == "Customer")
                    {
                        return RedirectToAction("Index", "Customer");
                    }
                    return RedirectToAction("Index", "Provider");
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Internal Server error. Please try later!!");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                TempData["Success"] = "Logged Out Successfully!!";
                HttpContext.Session.Clear();
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                string data = JsonConvert.SerializeObject(forgotPasswordModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync( baseAddress + "/Authentication/ForgotPassword", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Internal Server Error. Please try after some time!!");
                return View();
            }
            return View();
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            ResetPasswordModel resetPasswordModel = new ResetPasswordModel { Token = token, Email = email };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                string data = JsonConvert.SerializeObject(resetPasswordModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync( baseAddress + "/Authentication/ResetPassword", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(ResetPasswordConfirmation));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Internal Server Error. Please try after some time!!");
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}

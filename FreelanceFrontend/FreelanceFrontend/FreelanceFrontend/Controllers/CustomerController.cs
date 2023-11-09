using FreelanceFrontend.Models;
using FreelanceFrontend.Models.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FreelanceFrontend.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;
        Uri baseAddress = new Uri("http://localhost:7241/api");
        public CustomerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
            HttpResponseMessage response = await _httpClient.GetAsync( baseAddress + "/Service/GetAllServices");
            if(response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                List<Service> services = JsonConvert.DeserializeObject<List<Service>>(data);
                ContactServiceDto contact = new ContactServiceDto();
                contact.Services = services;
                return View(contact);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactProvider(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string data = JsonConvert.SerializeObject(contactViewModel);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
                    HttpResponseMessage response = await _httpClient.PostAsync( baseAddress + "/Customer/ContactProvider", content);
                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.Success = "Email Sent Successfully";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Success = "Some internal server error. Please try later";
                    return RedirectToAction("Index");
                }           
            }
            return RedirectToAction("Index");
        }
    }
}

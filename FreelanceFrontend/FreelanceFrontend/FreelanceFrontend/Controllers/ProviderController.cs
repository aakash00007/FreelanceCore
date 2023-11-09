using FreelanceFrontend.Models;
using FreelanceFrontend.Models.DtoModels;
using FreelanceFrontend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace FreelanceFrontend.Controllers
{
    [Authorize(Roles ="ServiceProvider")]
    public class ProviderController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:7241/api");
        private readonly HttpClient _httpClient;
        private readonly IClaimRepository _claimRepository;

        public ProviderController(HttpClient httpClient,IClaimRepository claimRepository)
        {
            _httpClient = httpClient;
            _claimRepository = claimRepository;
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
                ViewData["Error"] = null;
                return View(services);
            }
            ViewData["Error"] = "Service Not Found";
            return View();
        }      

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                ViewData["Error"] = "OOPS! Page doesn't exists";
                return View();
            }
            try
            {
                var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
                HttpResponseMessage response = await _httpClient.GetAsync( baseAddress + "/Service/GetServiceById/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    Service service = JsonConvert.DeserializeObject<Service>(data);
                    return View(service);
                }
                else
                {
                    ViewData["Error"] = "No data Found";
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Internal Server Error. Please try later!!";
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddService()
        {

            try
            {
                if (TempData["ServiceType"] == null)
                {
                    var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
                    HttpResponseMessage response = await _httpClient.GetAsync( baseAddress + "/ServiceType/GetServiceTypes");
                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        List<ServiceType> services = JsonConvert.DeserializeObject<List<ServiceType>>(data);
                        TempData["ServiceType"] = data;
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Internal Server Error. Please try later!!";
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddService(ServiceDto serviceDtoModel)
        {
            /*if (!ModelState.IsValid && files == null)
            {
                ModelState.AddModelError("", "Image File is Required");
                TempData.Keep();
                return View();
            }*/
            if (!ModelState.IsValid)
            {
                return View();
            }
            /*if (files == null)
            {
                TempData.Keep();
                ModelState.AddModelError("", "Image File is Required");
                return View();
            }*/

            try
            {
                var uri = new Uri(baseAddress+"/Service/AddService");
/*
                using var fileStream = new FileStream(
                    Path.Combine($"{_webHostEnvironment.ContentRootPath}//wwwroot//Images", $"{files.FileName}"),
                    FileMode.Create, FileAccess.Write);
                files.CopyTo(fileStream);*/
                string accessToken = HttpContext.Session.GetString("AccessToken");
                string userId = _claimRepository.GetClaims(accessToken).FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                serviceDtoModel.UserId = userId;
                string data = JsonConvert.SerializeObject(serviceDtoModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
                //formContent.Add(new StreamContent(files.OpenReadStream()), "file", Path.GetFileName(files.FileName));
                HttpResponseMessage response = await _httpClient.PostAsync(uri, content);
                if(response.IsSuccessStatusCode)
                {
                    ViewData["Success"] = "Service Added Successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Internal Server Error. Please try later!!";
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditService(int id)
        {
            try
            {
                var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
                HttpResponseMessage response = await _httpClient.GetAsync( baseAddress + "/Service/GetServiceById/" + id);
                if(response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    if (TempData["ServiceType"] == null)
                    {
                        HttpResponseMessage serviceTypeResponse = await _httpClient.GetAsync( baseAddress + "/ServiceType/GetServiceTypes"); 
                        if (serviceTypeResponse.IsSuccessStatusCode)
                        {
                            string serviceTypeData = await serviceTypeResponse.Content.ReadAsStringAsync();
                            List<ServiceType> serviceTypes = JsonConvert.DeserializeObject<List<ServiceType>>(serviceTypeData);
                            TempData["ServiceType"] = serviceTypeData;
                        }
                    }
                    ServiceDto serviceDtoModel = JsonConvert.DeserializeObject<ServiceDto>(data);
                    return View(serviceDtoModel);
                }
                else
                {
                    ViewData["Error"] = "Service Not Found";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Internal Server Error";
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditService(ServiceDto serviceDtoModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                //var formContent = new MultipartFormDataContent();
                //formContent.Add(new StringContent(serviceDtoModel.ServiceId.ToString()), "ServiceId");
                //formContent.Add(new StringContent(serviceDtoModel.Title), "Title");
                //formContent.Add(new StringContent(serviceDtoModel.Description), "Description");
                //formContent.Add(new StringContent(serviceDtoModel.Price.ToString()), "Price");
                //formContent.Add(new StringContent(serviceDtoModel.ServiceTypeId.ToString()), "ServiceTypeId");
                //formContent.Add(new StringContent(serviceDtoModel.UserId), "UserId");
                //if (files != null)
                //{
                //    using var fileStream = new FileStream(
                //    Path.Combine($"{_webHostEnvironment.ContentRootPath}//wwwroot//images", $"{files.FileName}"),
                //    FileMode.Create, FileAccess.Write);
                //    files.CopyTo(fileStream);
                //    formContent.Add(new StreamContent(files.OpenReadStream()), "file", Path.GetFileName(files.FileName));
                //}
                string data = JsonConvert.SerializeObject(serviceDtoModel);
                StringContent content = new StringContent(data,Encoding.UTF8,"Application/json");
                var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
                HttpResponseMessage response = await _httpClient.PutAsync( baseAddress + "/Service/UpdateService/" + serviceDtoModel.ServiceId, content);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Error"] = null;
                    ViewData["Success"] = "Service Updated Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Error"] = "Service Does not exists";
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Internal Server Error";
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                var accessTokenFromSession = HttpContext.Session.GetString("AccessToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenFromSession);
                HttpResponseMessage response = await _httpClient.DeleteAsync(baseAddress + "/Service/DeleteService/" + id);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Success"] = "Service Deleted Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Success"] = "Something went wrong. Please try later!!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Internal Server Error While Deleting. Please try later";
                return RedirectToAction("Index");
            }

        }
    }
}

using FreelanceDAL.Models;
using FreelanceDAL.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Bearer",Roles = "ServiceProvider")]
    public class ServiceTypeController : ControllerBase
    {
        private readonly IGenericRepository<ServiceType> _serviceTypeRepository;

        public ServiceTypeController(IGenericRepository<ServiceType> serviceTypeRepository)
        {
            _serviceTypeRepository = serviceTypeRepository;
        }

        [HttpGet]
        public IActionResult GetServiceTypes()
        {
            try
            {
                List<ServiceType> serviceTypes = _serviceTypeRepository.GetAll().Result.ToList();
                if (serviceTypes.Count == 0)
                {
                    return NotFound("No Service Type Found.");
                }
                return Ok(serviceTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}

using FreelanceDAL.Repository.Interfaces;
using FreelanceDAL.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FreelanceDAL.Models.DtoModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FreelanceBackend.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Bearer")]
    public class ServiceController : ControllerBase
    {
        private readonly IGenericRepository<Service> _serviceGenericRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceController(IGenericRepository<Service> serviceGenericRepository, IMapper mapper, IServiceRepository serviceRepository)
        {
            _serviceGenericRepository = serviceGenericRepository;
            _mapper = mapper;
            _serviceRepository = serviceRepository;
        }

        [HttpGet]
        [Authorize(Roles ="ServiceProvider,Customer")]
        public async Task<IActionResult> GetAllServices()
        {
            try
            {
                string role = User.FindFirstValue(ClaimTypes.Role);
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                List<Service> serviceList;
                if(role == "ServiceProvider")
                {
                    serviceList = _serviceRepository.GetServicesByUserId(userId).Result.ToList();
                    if (serviceList.Count() == 0)
                    {
                        return NotFound("No Services Available.");
                    }
                    return Ok(serviceList);
                }
                else if(role == "Customer")
                {
                    serviceList =  _serviceGenericRepository.GetAll().Result.ToList();
                    if (serviceList.Count() == 0)
                    {
                        return NotFound("No Services Available.");
                    }
                    return Ok(serviceList);
                }
                else { return BadRequest(); }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ServiceProvider")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            try
            {
                Service service = await _serviceGenericRepository.GetById(id);
                if (service == null)
                {
                    return NotFound($"Service with the Id:{id} doesn't exist");
                }
                return Ok(service);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = "ServiceProvider")]
        public async Task<IActionResult> AddService([FromBody] ServiceDto serviceDto)
        {
            try
            {
                var service = _mapper.Map<Service>(serviceDto);
                await _serviceGenericRepository.Insert(service);
                await _serviceGenericRepository.Save();
                return CreatedAtAction("GetServiceById", new { id = service.ServiceId }, service);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ServiceProvider")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] ServiceDto serviceDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var serviceExist = await _serviceGenericRepository.GetById(id);
                    if (serviceExist == null)
                    {
                        return NotFound("Service Not Found.");
                    }
                    var service = _mapper.Map(serviceDto, serviceExist);
                    _serviceGenericRepository.Update(serviceExist);
                    await _serviceGenericRepository.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ServiceProvider")]
        public async Task<IActionResult> DeleteService(int id)
        {
            Service serviceExist = await _serviceGenericRepository.GetById(id);
            if(serviceExist == null)
            {
                return NotFound("Service Not Found");
            }
            await _serviceGenericRepository.Delete(serviceExist.ServiceId);
            await _serviceGenericRepository.Save();
            return Ok();
        }


    }
}

using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lyzer_BE.API.Controllers
{
    [Route("api/driver")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        // GET api/driver/5
        [HttpGet("{id}")]
        public DriverDTO? GetDriver(int id)
        {
            return _driverService.GetDriver(id);
        }
    }
}

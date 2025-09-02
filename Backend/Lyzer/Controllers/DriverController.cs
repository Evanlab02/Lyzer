using Lyzer.Common.DTO;
using Lyzer.Services;

using Microsoft.AspNetCore.Mvc;


namespace Lyzer.Controllers
{
    [ApiController]
    [Route("/api/v1/drivers")]
    public class DriverController(ILogger<DriverController> logger, DriverService driverService) : ControllerBase
    {
        private readonly ILogger<DriverController> _logger = logger;
        private readonly DriverService _driverService = driverService;

        [HttpGet("standings/current", Name = "Get driver standings")]
        public async Task<DriverStandingsDTO> GetCurrentDriverStandings()
        {
            return await _driverService.GetCachedDriverStandings("current");
        }
    }
}
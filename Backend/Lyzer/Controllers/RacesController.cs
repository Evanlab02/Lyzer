using Lyzer.Common.DTO;
using Lyzer.Services;

using Microsoft.AspNetCore.Mvc;


namespace Lyzer.Controllers
{
    [ApiController]
    [Route("/api/v1/races")]
    public class RacesController : ControllerBase
    {

        private readonly ILogger<RacesController> _logger;
        private readonly RacesService _racesService;

        public RacesController(ILogger<RacesController> logger, RacesService racesService)
        {
            _logger = logger;
            _racesService = racesService;
        }

        [HttpGet("current", Name = "GetSeasonRaces")]
        public async Task<RacesDTO> GetSeasonRaces()
        {
            return await _racesService.GetCachedRaces("current");
        }
    }
}

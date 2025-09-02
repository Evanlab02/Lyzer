using Lyzer.Common.DTO;
using Lyzer.Services;

using Microsoft.AspNetCore.Mvc;


namespace Lyzer.Controllers
{
    [ApiController]
    [Route("/api/v1/races")]
    public class RacesController(ILogger<RacesController> logger, RacesService racesService) : ControllerBase
    {

        private readonly ILogger<RacesController> _logger = logger;
        private readonly RacesService _racesService = racesService;

        [HttpGet("current", Name = "GetSeasonRaces")]
        public async Task<RacesDTO> GetSeasonRaces()
        {
            return await _racesService.GetCachedRaces("current");
        }
    }
}
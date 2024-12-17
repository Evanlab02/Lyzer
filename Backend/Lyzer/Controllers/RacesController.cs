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

        [HttpGet("", Name = "GetRaces")]
        public RacesDTO GetRaces()
        {
            return _racesService.GetRaces();
        }
    }
}
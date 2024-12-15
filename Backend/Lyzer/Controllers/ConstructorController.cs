using Lyzer.Common.DTO;
using Lyzer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lyzer.Controllers
{
    [ApiController]
    [Route("/api/v1/constructors")]
    public class ConstructorController : ControllerBase
    {
        private readonly ILogger<ConstructorController> _logger;
        private readonly ConstructorService _constructorService;

        public ConstructorController(ILogger<ConstructorController> logger, ConstructorService constructorService)
        {
            _logger = logger;
            _constructorService = constructorService;
        }

        [HttpGet("standings/current", Name = "Get constructors standings for current year")]
        public async Task<ConstructorStandingsDTO> GetCurrentYearStandings(string year)
        {
            return await _constructorService.GetCachedConstructorStandings(year);
        }
    }
}

using Lyzer.Common.DTO;
using Lyzer.Services;

using Microsoft.AspNetCore.Mvc;

namespace Lyzer.Controllers
{
    [ApiController]
    [Route("/api/v1/constructors")]
    public class ConstructorController(ILogger<ConstructorController> logger, ConstructorService constructorService) : ControllerBase
    {
        private readonly ILogger<ConstructorController> _logger = logger;
        private readonly ConstructorService _constructorService = constructorService;

        [HttpGet("standings/current", Name = "Get constructors standings for current year")]
        public async Task<ConstructorStandingsDTO> GetCurrentYearStandings()
        {
            return await _constructorService.GetCachedConstructorStandings("current");
        }
    }
}
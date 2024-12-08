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

        [HttpGet("standings", Name = "Get constructors standings for specified year")]
        public async Task<ConstructorStandingsDTO> GetConstructorsStandingsForYear()
        {
            return await _constructorService.GetConstructorsStandingsForYear("current");
        }
    }
}

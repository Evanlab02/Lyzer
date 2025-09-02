using Lyzer.Common.DTO;
using Lyzer.Services;

using Microsoft.AspNetCore.Mvc;


namespace Lyzer.Controllers
{
    [ApiController]
    [Route("/api/v1/results")]
    public class ResultsController(ILogger<ResultsController> logger, ResultsService resultsService) : ControllerBase
    {
        private readonly ILogger<ResultsController> _logger = logger;
        private readonly ResultsService _resultsService = resultsService;

        [HttpGet("current/{round}", Name = "GetLatestRaceResult")]
        public async Task<ResultsDTO> GetLatestRaceResult(string round)
        {
            return await _resultsService.GetCachedRaceResult("current", round);
        }
    }
}
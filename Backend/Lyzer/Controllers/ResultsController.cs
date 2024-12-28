using Lyzer.Common.DTO;
using Lyzer.Services;
using Microsoft.AspNetCore.Mvc;


namespace Lyzer.Controllers
{
    [ApiController]
    [Route("/api/v1/results")]
    public class ResultsController : ControllerBase
    {
        private readonly ILogger<ResultsController> _logger;
        private readonly ResultsService _resultsService;

        public ResultsController(ILogger<ResultsController> logger, ResultsService resultsService)
        {
            _logger = logger;
            _resultsService = resultsService;
        }

        [HttpGet("current/{round}", Name = "GetLatestRaceResult")]
        public async Task<ResultsDTO> GetLatestRaceResult(string round)
        {
            return await _resultsService.GetCachedRaceResult("current", round);
        }
    }
}

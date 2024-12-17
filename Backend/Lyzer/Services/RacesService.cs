namespace Lyzer.Services
{
    public class RacesService
    {
        private readonly ILogger<RacesService> _logger;
        public RacesService(ILogger<RacesService> logger)
        {
            _logger = logger;
        }
        public RacesDTO GetRaces()
        {
            _logger.LogInformation("GetRaces called");
            return new RacesDTO() { Value = "test" };
        }
    }
}
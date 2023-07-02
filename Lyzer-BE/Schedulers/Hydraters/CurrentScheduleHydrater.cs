using Lyzer_BE.API.Services.Concrete;
using Lyzer_BE.API.Services.Interfaces;

namespace Lyzer_BE.Schedulers.Hydraters
{
    public class CurrentScheduleHydrater : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IHydrationService hydrationService = new HydrationService();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromDays(30), stoppingToken); // Delay between each run
                hydrationService.HydrateCurrentSchedule();
                hydrationService.HydrateFollowingYearSchedule();
            }
        }
    }
}

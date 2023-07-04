using Lyzer_BE.API.Services.Concrete;
using Lyzer_BE.API.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Lyzer_BE.Schedulers.Hydraters
{
    [ExcludeFromCodeCoverage]
    public class CurrentScheduleHydrater : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IHydrationService hydrationService = HydrationService.Instance;

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromDays(30), stoppingToken); // Delay between each run
                var year = DateTime.Now.Year;
                hydrationService.HydrateSchedule(year.ToString());
                hydrationService.HydrateSchedule((year + 1).ToString());
            }
        }
    }
}

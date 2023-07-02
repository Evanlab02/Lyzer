using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Concrete;
using Lyzer_BE.API.Services.Interfaces;

namespace Lyzer_BE.Tests.API.Services
{
    [TestFixture]
    public class TestScheduleService
    {
        [Test]
        public void GetFullSchedule_ShouldReturn22Events()
        {
            //Uncomment once we figure out how to use local mongoDB Docker image for tests
            //IScheduleService scheduleService = new ScheduleService(true);
            //List<RaceWeekendDTO> events = scheduleService.GetFullSchedule().Result;
            //Assert.That(events.Count, Is.EqualTo(22));
        }

        [Test]
        public void GetNextOrCurrentEvent()
        {
            //Uncomment once we figure out how to use local mongoDB Docker image for tests
            //var today = DateTime.Now;
            //IScheduleService scheduleService = new ScheduleService(true);
            //RaceWeekendDTO events = scheduleService.GetNextOrCurrentEvent().Result;
            //var endDateTime = DateTime.Parse(events.Date);
            //Assert.That(today, Is.LessThan(endDateTime));
        }
    }
}

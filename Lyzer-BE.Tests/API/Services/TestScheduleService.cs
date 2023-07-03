using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Concrete;
using Lyzer_BE.API.Services.Interfaces;
using Lyzer_BE.Database;
using MongoDB.Driver;

namespace Lyzer_BE.Tests.API.Services
{
    [TestFixture]
    public class TestScheduleService
    {
        MongoController<RaceWeekendDTO> _mongoController;
        DateTime _today = DateTime.Now;

        [SetUp]
        public void Setup()
        {

            Environment.SetEnvironmentVariable("MONGODB_CONNECTION", "mongodb://localhost:27017");
            var year = DateTime.Now.Year.ToString();
            _mongoController = new("Schedules", year);

            var beforeToday = _today.AddDays(-1);
            var afterToday = _today.AddDays(1);

            _mongoController.InsertManyIntoCollection(new List<RaceWeekendDTO>
            {
                new()
                {
                    Date = $"{beforeToday.Year}-{beforeToday.Month}-{beforeToday.Day}",
                    Time = $"{beforeToday.TimeOfDay}",
                    Round = "1"
                },
                new()
                {
                    Date = $"{afterToday.Year}-{afterToday.Month}-{afterToday.Day}",
                    Time = $"{afterToday.TimeOfDay}",
                    Round = "2"
                },
            });

            _mongoController.SetCollection("2021");
            _mongoController.InsertManyIntoCollection(new List<RaceWeekendDTO>
            {
                new()
                {
                    Date = $"2021-01-01",
                    Time = "00:00:00",
                },
            });
        }

        [TearDown]
        public void TearDown()
        {
            _mongoController.SetCollection(DateTime.Now.Year.ToString());
            _mongoController.DeleteManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);
            _mongoController.SetCollection("2021");
            _mongoController.DeleteManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);
        }

        [Test]
        public void GetFullScheduleCurrentYear_ShouldReturn2Events()
        {
            HydrationService hydrationService = new();
            IScheduleService scheduleService = new ScheduleService(hydrationService);
            List<RaceWeekendDTO> events = scheduleService.GetFullSchedule("current").Result;
            Assert.That(events.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetFullSchedule2021Year_ShouldReturn1Event()
        {
            var year = "2021";
            HydrationService hydrationService = new();
            IScheduleService scheduleService = new ScheduleService(hydrationService);
            List<RaceWeekendDTO> events = scheduleService.GetFullSchedule(year).Result;
            Assert.That(events.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetFullScheduleEmptyYear_ShouldReturnEmptySchedule()
        {
            HydrationService hydrationService = new();
            IScheduleService scheduleService = new ScheduleService(hydrationService);
            List<RaceWeekendDTO> events = scheduleService.GetFullSchedule("").Result;
            Assert.That(events.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetFullScheduleBeforeYear1950_ShouldReturnEmptySchedule()
        {
            HydrationService hydrationService = new();
            IScheduleService scheduleService = new ScheduleService(hydrationService);
            List<RaceWeekendDTO> events = scheduleService.GetFullSchedule("1949").Result;
            Assert.That(events.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetFullScheduleAfter2YearsInFuture_ShouldReturnEmptySchedule()
        {
            var year = _today.AddYears(2).Year.ToString();
            HydrationService hydrationService = new();
            IScheduleService scheduleService = new ScheduleService(hydrationService);
            List<RaceWeekendDTO> events = scheduleService.GetFullSchedule(year).Result;
            Assert.That(events.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetNextOrCurrentEvent_ShouldReturnNextEvent()
        {
            var eventDate = DateTime.Now.AddDays(1);
            HydrationService hydrationService = new();
            IScheduleService scheduleService = new ScheduleService(hydrationService);
            RaceWeekendDTO nextEvent = scheduleService.GetNextOrCurrentRaceWeekend().Result;
            Assert.That(nextEvent.Date, Is.EqualTo($"{eventDate.Year}-{eventDate.Month}-{eventDate.Day}"));
            Assert.That(nextEvent.Round, Is.EqualTo("2"));
        }
    }
}

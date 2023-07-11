using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Concrete;
using Lyzer_BE.API.Services.Interfaces;
using Lyzer_BE.Database;
using MongoDB.Driver;
using RestSharp;
using RichardSzalay.MockHttp;

namespace Lyzer_BE.Tests.API.Services
{
    [TestFixture]
    internal class TestScheduleService
    {
        MongoController<RaceWeekendDTO> _mongoController;
        HydrationService hydrationService = new();
        DateTime _today = DateTime.Now;

        [SetUp]
        public void Setup()
        {
            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("MONGODB_CONNECTION")))
                Environment.SetEnvironmentVariable("MONGODB_CONNECTION", "mongodb://localhost:27017");

            var year = _today.Year.ToString();
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
        }

        [TearDown]
        public void TearDown()
        {
            _mongoController.SetCollection(_today.Year.ToString());
            _mongoController.DestroyCollection();
            _mongoController.SetCollection(_today.AddYears(1).Year.ToString());
            _mongoController.DestroyCollection();
            _mongoController.SetCollection("2021");
            _mongoController.DestroyCollection();
        }

        [Test]
        public void GetFullScheduleCurrentYear_ShouldReturn2Events()
        {

            IScheduleService scheduleService = new ScheduleService(hydrationService);
            List<RaceWeekendDTO> events = scheduleService.GetFullSchedule("current").Result;
            Assert.That(events.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetFullSchedule2021Year_ShouldReturn2EventsAndHydrateCollection()
        {
            var mockHttp = new MockHttpMessageHandler();
            var jsonSchedule = "{\"MRData\":{\"RaceTable\":{\"Races\":[{\"Round\":\"1\",\"RaceName\":null,\"Date\":\"2021-04-01\",\"Time\":\"15:00:00Z\",\"FirstPractice\":null,\"SecondPractice\":null,\"ThirdPractice\":null,\"Qualifying\":null,\"Sprint\":null},{\"Round\":\"2\",\"RaceName\":null,\"Date\":\"2021-05-02\",\"Time\":\"16:00:00Z\",\"FirstPractice\":null,\"SecondPractice\":null,\"ThirdPractice\":null,\"Qualifying\":null,\"Sprint\":null}]}}}";

            mockHttp.When("http://ergast.com/api/f1/2021.json")
                    .Respond("application/json", jsonSchedule);

            var client = new RestClient(new RestClientOptions("http://ergast.com/api/f1/") { ConfigureMessageHandler = _ => mockHttp });
            hydrationService.SetRestClient(client);

            IScheduleService scheduleService = new ScheduleService(hydrationService);
            List<RaceWeekendDTO> events = scheduleService.GetFullSchedule("2021").Result;
            Assert.That(events.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetNextOrCurrentEvent_ShouldReturnNextEvent()
        {
            var yearFromNow = DateTime.Now.AddYears(1).Year.ToString();
            var mockHttp = new MockHttpMessageHandler();
            var jsonSchedule = "{\"MRData\":{\"RaceTable\":{\"Races\":[]}}}";
            mockHttp.When($"http://ergast.com/api/f1/{yearFromNow}.json")
                   .Respond("application/json", jsonSchedule);

            var client = new RestClient(new RestClientOptions("http://ergast.com/api/f1/") { ConfigureMessageHandler = _ => mockHttp });
            hydrationService.SetRestClient(client);

            var eventDate = DateTime.Now.AddDays(1);
            IScheduleService scheduleService = new ScheduleService(hydrationService);
            RaceWeekendDTO nextEvent = scheduleService.GetNextOrCurrentRaceWeekend().Result;
            Assert.That(nextEvent.Date, Is.EqualTo($"{eventDate.Year}-{eventDate.Month}-{eventDate.Day}"));
            Assert.That(nextEvent.Round, Is.EqualTo("2"));
            _mongoController.SetCollection(yearFromNow);
            Assert.That(_mongoController.CollectionExists(), Is.True);
        }

        [Test]
        public void GetFullScheduleBeforeYear1950_ShouldReturnEmptySchedule()
        {
            IScheduleService scheduleService = new ScheduleService(hydrationService);
            List<RaceWeekendDTO> events = scheduleService.GetFullSchedule("1949").Result;
            Assert.That(events.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetFullScheduleAfter2YearsInFuture_ShouldReturnEmptySchedule()
        {
            var year = _today.AddYears(2).Year.ToString();
            IScheduleService scheduleService = new ScheduleService(hydrationService);
            List<RaceWeekendDTO> events = scheduleService.GetFullSchedule(year).Result;
            Assert.That(events.Count, Is.EqualTo(0));
        }

    }
}

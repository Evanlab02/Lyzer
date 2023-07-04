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
    public class TestScheduleAndHydrationService
    {
        MongoController<RaceWeekendDTO> _mongoController;
        HydrationService hydrationService = HydrationService.Instance;
        DateTime _today = DateTime.Now;

        [SetUp]
        public void Setup()
        {
            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("MONGODB_CONNECTION")))
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
            _mongoController.SetCollection("2001");
            _mongoController.DeleteManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);
            _mongoController.SetCollection("2008");
            _mongoController.DeleteManyFromCollection(Builders<RaceWeekendDTO>.Filter.Empty);
        }

        [Test]
        public void GetFullScheduleCurrentYear_ShouldReturn2Events()
        {

            IScheduleService scheduleService = new ScheduleService(hydrationService);
            List<RaceWeekendDTO> events = scheduleService.GetFullSchedule("current").Result;
            Assert.That(events.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetFullSchedule2021Year_ShouldReturn1Event()
        {
            var year = "2021";
            IScheduleService scheduleService = new ScheduleService(hydrationService);
            List<RaceWeekendDTO> events = scheduleService.GetFullSchedule(year).Result;
            Assert.That(events.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetFullScheduleEmptyYear_ShouldReturnEmptySchedule()
        {
            IScheduleService scheduleService = new ScheduleService(hydrationService);
            List<RaceWeekendDTO> events = scheduleService.GetFullSchedule("").Result;
            Assert.That(events.Count, Is.EqualTo(0));
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

        [Test]
        public void GetNextOrCurrentEvent_ShouldReturnNextEvent()
        {
            var eventDate = DateTime.Now.AddDays(1);
            IScheduleService scheduleService = new ScheduleService(hydrationService);
            RaceWeekendDTO nextEvent = scheduleService.GetNextOrCurrentRaceWeekend().Result;
            Assert.That(nextEvent.Date, Is.EqualTo($"{eventDate.Year}-{eventDate.Month}-{eventDate.Day}"));
            Assert.That(nextEvent.Round, Is.EqualTo("2"));
        }

        [Test]
        public async Task GetFullScheduleWithEmptyCollection_ShouldReturnFullSchedule()
        {
            var mockHttp = new MockHttpMessageHandler();

            var jsonSchedule = "{\"MRData\":{\"RaceTable\":{\"Races\":[{\"Round\":\"1\",\"RaceName\":null,\"Date\":\"2001-04-01\",\"Time\":\"15:00:00Z\",\"FirstPractice\":null,\"SecondPractice\":null,\"ThirdPractice\":null,\"Qualifying\":null,\"Sprint\":null},{\"Round\":\"2\",\"RaceName\":null,\"Date\":\"2001-05-02\",\"Time\":\"16:00:00Z\",\"FirstPractice\":null,\"SecondPractice\":null,\"ThirdPractice\":null,\"Qualifying\":null,\"Sprint\":null}]}}}";

            mockHttp.When("http://ergast.com/api/f1/2001.json")
                    .Respond("application/json", jsonSchedule); // Respond with JSON

            var client = new RestClient(new RestClientOptions("http://ergast.com/api/f1/") { ConfigureMessageHandler = _ => mockHttp });

            hydrationService.SetRestClient(client);

            //TODO: Make schedule service class wide
            IScheduleService scheduleService = new ScheduleService(hydrationService);
            List<RaceWeekendDTO> events = await scheduleService.GetFullSchedule("2001");

            Assert.That(events.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task HydrateSchedule2008_ShouldReturnEntireResponse()
        {
            var mockHttp = new MockHttpMessageHandler();

            var jsonSchedule = "{\"MRData\":{\"RaceTable\":{\"Races\":[{\"Round\":\"1\",\"RaceName\":null,\"Date\":\"2008-04-01\",\"Time\":\"15:00:00Z\",\"FirstPractice\":null,\"SecondPractice\":null,\"ThirdPractice\":null,\"Qualifying\":null,\"Sprint\":null},{\"Round\":\"2\",\"RaceName\":null,\"Date\":\"2008-05-02\",\"Time\":\"16:00:00Z\",\"FirstPractice\":null,\"SecondPractice\":null,\"ThirdPractice\":null,\"Qualifying\":null,\"Sprint\":null}]}}}";

            mockHttp.When("http://ergast.com/api/f1/2008.json")
                    .Respond("application/json", jsonSchedule); // Respond with JSON

            // Instantiate the client normally, but replace the message handler
            var client = new RestClient(new RestClientOptions("http://ergast.com/api/f1/") { ConfigureMessageHandler = _ => mockHttp });

            hydrationService.SetRestClient(client);

            ScheduleDTO schedule = await hydrationService.HydrateSchedule("2008");

            Assert.That(schedule.ScheduleData.ScheduleTable.RaceWeekends.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task HydrateSchedule2009_ShouldReturnEmptyRaceWeekendArray()
        {
            var mockHttp = new MockHttpMessageHandler();

            var jsonSchedule = "{\"MRData\":{\"RaceTable\":{\"Races\":[]}}}";

            mockHttp.When("http://ergast.com/api/f1/2009.json")
                    .Respond("application/json", jsonSchedule); // Respond with JSON

            // Instantiate the client normally, but replace the message handler
            var client = new RestClient(new RestClientOptions("http://ergast.com/api/f1/") { ConfigureMessageHandler = _ => mockHttp });

            hydrationService.SetRestClient(client);

            ScheduleDTO schedule = await hydrationService.HydrateSchedule("2009");

            Assert.That(schedule.ScheduleData.ScheduleTable.RaceWeekends.Count, Is.EqualTo(0));
        }
    }
}

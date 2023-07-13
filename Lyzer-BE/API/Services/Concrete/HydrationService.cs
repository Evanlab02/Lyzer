using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Lyzer_BE.Database;
using MongoDB.Driver;
using RestSharp;

namespace Lyzer_BE.API.Services.Concrete
{
    public sealed class HydrationService : IHydrationService
    {
        private static RestClient _restClient;
        public HydrationService()
        {
            _restClient = new RestClient(new RestClientOptions("http://ergast.com/api/f1/"));
        }

        public static HydrationService Instance { get { return Nested.instance; } }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly HydrationService instance = new HydrationService();
        }

        public void SetRestClient(RestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<ScheduleDTO> HydrateSchedule(string year)
        {
            var request = new RestRequest($"{year}.json");
            var response = await _restClient.GetAsync<ScheduleDTO>(request);

            MongoController<RaceWeekendDTO> mongoController = new("Schedules", year);
            if (!mongoController.CollectionExists())
            {
                mongoController.CreateCollection();
                Console.WriteLine("Created collection for year: " + year);
            }

            if (response.ScheduleData.ScheduleTable.RaceWeekends.Count > 0)
            {
                var filterValues = Builders<RaceWeekendDTO>.Filter.Empty;
                mongoController.DeleteManyFromCollection(filterValues);
                mongoController.InsertManyIntoCollection(response.ScheduleData.ScheduleTable.RaceWeekends);
                Console.WriteLine("Hydrated schedule for year: " + year);
            }

            return response;
        }
    }
}

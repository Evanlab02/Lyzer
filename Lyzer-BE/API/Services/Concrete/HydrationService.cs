using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Lyzer_BE.Database;
using MongoDB.Driver;
using Newtonsoft.Json;
using RestSharp;

namespace Lyzer_BE.API.Services.Concrete
{
    public sealed class HydrationService : IHydrationService
    {
        private static RestClient _restClient = new(new RestClientOptions("http://ergast.com/api/f1/"));

        public static HydrationService Instance { get { return Nested.instance; } }

        private sealed class Nested
        {
            static Nested()
            {
            }

            internal static readonly HydrationService instance = new();
        }

        public static void SetRestClient(RestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<ScheduleDTO> HydrateSchedule(string year)
        {
            var request = new RestRequest($"{year}.json");
            var response = await _restClient.GetAsync<ScheduleDTO>(request);
            Console.WriteLine(JsonConvert.SerializeObject(response));
            //TODO: Move duplicated code into method that can be used by other hydration methods.
            if (response.ScheduleData.ScheduleTable.RaceWeekends.Count > 0)
            {
                MongoController<RaceWeekendDTO> mongoController = new("Schedules", year);
                var filterValues = Builders<RaceWeekendDTO>.Filter.Empty;
                mongoController.DeleteManyFromCollection(filterValues);
                mongoController.InsertManyIntoCollection(response.ScheduleData.ScheduleTable.RaceWeekends);
            }

            return response;
        }
    }
}

using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Lyzer_BE.Database;
using MongoDB.Driver;
using RestSharp;

namespace Lyzer_BE.API.Services.Concrete
{
    public class HydrationService : IHydrationService
    {
        public async Task<ScheduleDTO> HydrateCurrentSchedule()
        {
            var options = new RestClientOptions("http://ergast.com/api/f1/");
            var client = new RestClient(options);
            var request = new RestRequest($"current.json");
            var response = await client.GetAsync<ScheduleDTO>(request);

            MongoController<RaceWeekendDTO> mongoController = new("Current Schedules");
            var filterValues = Builders<RaceWeekendDTO>.Filter.Empty;
            mongoController.DeleteManyFromCollection(filterValues);

            mongoController.InsertManyIntoCollection(response.ScheduleData.ScheduleTable.RaceWeekends);
            return response;
        }
    }
}

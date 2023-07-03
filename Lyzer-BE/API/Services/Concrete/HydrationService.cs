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

            MongoController<RaceWeekendDTO> mongoController = new("Schedules", DateTime.Now.Year.ToString());
            var filterValues = Builders<RaceWeekendDTO>.Filter.Empty;
            mongoController.DeleteManyFromCollection(filterValues);

            mongoController.InsertManyIntoCollection(response.ScheduleData.ScheduleTable.RaceWeekends);

            return response;
        }

        public async Task<ScheduleDTO> HydrateFollowingYearSchedule()
        {
            var nextYear = DateTime.Now.AddYears(1).Year.ToString();

            var options = new RestClientOptions("http://ergast.com/api/f1/");
            var client = new RestClient(options);
            var request = new RestRequest($"{nextYear}.json");
            var response = await client.GetAsync<ScheduleDTO>(request);

            if (response.ScheduleData.ScheduleTable.RaceWeekends.Count > 0)
            {
                MongoController<RaceWeekendDTO> mongoController = new("Schedules", nextYear);
                var filterValues = Builders<RaceWeekendDTO>.Filter.Empty;
                mongoController.DeleteManyFromCollection(filterValues);

                if (response.ScheduleData.ScheduleTable.RaceWeekends.Count != 0)
                {
                    mongoController.InsertManyIntoCollection(response.ScheduleData.ScheduleTable.RaceWeekends);
                }
            }

            return response;
        }

        public async Task<ScheduleDTO> HydrateSchedule(string year)
        {
            var options = new RestClientOptions("http://ergast.com/api/f1/");
            var client = new RestClient(options);
            var request = new RestRequest($"{year}.json");
            var response = await client.GetAsync<ScheduleDTO>(request);

            //TODO: Move duplicated code into method that can be used by other hydration methods.
            if (response.ScheduleData.ScheduleTable.RaceWeekends.Count > 0)
            {
                MongoController<RaceWeekendDTO> mongoController = new("Schedules", year);
                var filterValues = Builders<RaceWeekendDTO>.Filter.Empty;
                mongoController.DeleteManyFromCollection(filterValues);

                if (response.ScheduleData.ScheduleTable.RaceWeekends.Count != 0)
                {
                    mongoController.InsertManyIntoCollection(response.ScheduleData.ScheduleTable.RaceWeekends);
                }
            }

            return response;
        }
    }
}

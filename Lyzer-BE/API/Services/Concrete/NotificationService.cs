using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Enums;
using Lyzer_BE.API.Services.Interfaces;
using RestSharp;

namespace Lyzer_BE.API.Services.Concrete
{
    public class NotificationService : INotificationService
    {
        public string ntfy = "https://ntfy.sh/";

        public async void SendAlert(AlertDto alert, AlertLevel alertLevel)
        {
            bool success = false;

            if (alertLevel.Equals(AlertLevel.Critical))
                success = await SendCriticalAlert(alert);
            
            if (alertLevel.Equals(AlertLevel.NonCritical)) 
                success = await SendNonCriticalAlert(alert);

            if (!success)
                throw new Exception($"Failed to send alert with message: {alert.Message}");
        }

        private async Task<bool> SendCriticalAlert(AlertDto alert) 
        {
            var client = new RestClient(ntfy);
            var request = new RestRequest(AlertChannel.Critical, Method.Post);

            request.AddJsonBody(alert);
            
            var response = await client.PostAsync(request);

            return response.IsSuccessful;
        }

        private async Task<bool> SendNonCriticalAlert(AlertDto alert) 
        {
            var client = new RestClient(ntfy);
            var request = new RestRequest(AlertChannel.NonCritical, Method.Post);

            //Json doesn't look so great. Maybe we can look into making it a better looking string?
            request.AddJsonBody(alert);

            var response = await client.PostAsync(request);

            return response.IsSuccessful;
        }
    }
}

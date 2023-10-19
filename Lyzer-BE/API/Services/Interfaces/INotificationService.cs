using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Enums;

namespace Lyzer_BE.API.Services.Interfaces
{
    public interface INotificationService
    {
        public void SendAlert(AlertDto alert, AlertLevel alertLevel);
    }
}

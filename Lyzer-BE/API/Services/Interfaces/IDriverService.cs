using Lyzer_BE.API.DTOs;

namespace Lyzer_BE.API.Services.Interfaces
{
    public interface IDriverService
    {
        public DriverDTO? GetDriver(int id);
    }
}

using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;

namespace Lyzer_BE.API.Services.Concrete
{
    public class DriverService : IDriverService
    {
        List<DriverDTO> drivers = new List<DriverDTO>
        {
            new DriverDTO
            {
                Id = 1,
                Name = "Max Verstappen",
                Age = 26,
                Points = 130,
                Constructer = "Red Bull Racing"
            },
        };

        public DriverDTO? GetDriver(int id)
        {
            return drivers.FirstOrDefault(x => x.Id == id);
        }
    }
}

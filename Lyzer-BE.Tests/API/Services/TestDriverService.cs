using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Concrete;
using Lyzer_BE.API.Services.Interfaces;

namespace Lyzer_BE.Tests.API.Services
{
    [TestFixture]
    public class TestDriverService
    {
        [Test]
        public void GetDriverWithIdOne_ShouldReturnDriverOne()
        {
            IDriverService driverService = new DriverService();
            DriverDTO driver = driverService.GetDriver(1);
            Assert.That(driver.Id, Is.EqualTo(1));
            Assert.That(driver.Name, Is.EqualTo("Max Verstappen"));
            Assert.That(driver.Age, Is.EqualTo(26));
            Assert.That(driver.Points, Is.EqualTo(130));
            Assert.That(driver.Constructer, Is.EqualTo("Red Bull Racing"));
        }
    }
}

using Lyzer_BE.API.Controllers;
using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Moq;

namespace Lyzer_BE.API.Tests
{
    [TestFixture]
    public class DriverControllerTests
    {
        [Test]
        public void GetDriver_ShouldReturnDriver()
        {
            // Arrange
            var mockedDriver = new DriverDTO { Id = 1, Name = "Mocked Driver" };
            var driverServiceMock = new Mock<IDriverService>();
            var driverController = new DriverController(driverServiceMock.Object);

            driverServiceMock.Setup(s => s.GetDriver(It.Is<int>(id => id == 1)))
                .Returns(mockedDriver);

            DriverDTO? result = driverController.GetDriver(1);
            Assert.AreEqual(mockedDriver, result);
        }
    }
}


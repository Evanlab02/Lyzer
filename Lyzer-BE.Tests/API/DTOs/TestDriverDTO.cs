using Lyzer_BE.API.DTOs;

namespace Lyzer_BE.Tests.API.DTOs
{
    [TestFixture]
    public class TestDriverDTO
    {
        [Test]
        public void GetId_ShouldReturnID()
        {
            var driverDTO = new DriverDTO();
            driverDTO.Id = 1;
            Assert.That(driverDTO.Id, Is.EqualTo(1));
        }

        [Test]
        public void GetName_ShouldReturnName()
        {
            var driverDTO = new DriverDTO();
            driverDTO.Name = "Charles Leclerc";
            Assert.That(driverDTO.Name, Is.EqualTo("Charles Leclerc"));
        }

        [Test]
        public void GetAge_ShouldReturnAge()
        {
            var driverDTO = new DriverDTO();
            driverDTO.Age = 30;
            Assert.That(driverDTO.Age, Is.EqualTo(30));
        }

        [Test]
        public void GetPoints_ShouldReturnPoints()
        {
            var driverDTO = new DriverDTO();
            driverDTO.Points = 20;
            Assert.That(driverDTO.Points, Is.EqualTo(20));
        }

        [Test]
        public void GetConstructor_ShouldReturnConstructor()
        {
            var driverDTO = new DriverDTO();
            driverDTO.Constructer = "Ferrari";
            Assert.That(driverDTO.Constructer, Is.EqualTo("Ferrari"));
        }
    }
}

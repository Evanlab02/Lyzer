using Lyzer_BE.API.DTOs;

namespace Lyzer_BE.Tests.API.DTOs
{
    [TestFixture]
    public class TestSessionDTO
    {
        [Test]
        public void GetDate_ShouldReturnDate()
        {
            SessionDTO sessionDTO = new SessionDTO();
            sessionDTO.Date = "2023-03-28";
            Assert.That(sessionDTO.Date, Is.EqualTo("2023-03-28"));
        }

        [Test]
        public void GetTime_ShouldReturnTime()
        {
            SessionDTO sessionDTO = new SessionDTO();
            sessionDTO.Time = "17:00:00Z";
            Assert.That(sessionDTO.Time, Is.EqualTo("17:00:00Z"));
        }
    }

    [TestFixture]
    public class TestEventDTO
    {
        [Test]
        public void GetDate_ShouldReturnDate()
        {
            RaceWeekendDTO EventDTO = new RaceWeekendDTO();
            EventDTO.Date = "2023-03-28";
            Assert.That(EventDTO.Date, Is.EqualTo("2023-03-28"));
        }

        [Test]
        public void GetTime_ShouldReturnTime()
        {
            RaceWeekendDTO EventDTO = new RaceWeekendDTO();
            EventDTO.Time = "17:00:00Z";
            Assert.That(EventDTO.Time, Is.EqualTo("17:00:00Z"));
        }

        [Test]
        public void GetRound_ShouldReturnRound()
        {
            RaceWeekendDTO EventDTO = new RaceWeekendDTO();
            EventDTO.Round = "1";
            Assert.That(EventDTO.Round, Is.EqualTo("1"));
        }

        [Test]
        public void GetRaceName_ShouldReturnRaceName()
        {
            RaceWeekendDTO EventDTO = new RaceWeekendDTO();
            EventDTO.RaceName = "Bahrain";
            Assert.That(EventDTO.RaceName, Is.EqualTo("Bahrain"));
        }
    }
}

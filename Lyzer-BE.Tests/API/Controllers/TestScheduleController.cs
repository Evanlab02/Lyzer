using Lyzer_BE.API.Controllers;
using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Moq;

namespace Lyzer_BE.API.Tests
{
    [TestFixture]
    public class ScheduleControllerTests
    {
        [Test]
        public void GetSchedule_ShouldReturnEntireSchedule()
        {
            // Arrange

            var Events = new List<RaceWeekendDTO> {
                new RaceWeekendDTO {
                    Date = "2021-03-28",
                    Time = "15:00:00Z",
                    FirstPractice = new SessionDTO {
                        Date = "2021-03-26",
                        Time = "10:30:00Z"
                    },
                    SecondPractice = new SessionDTO {
                        Date = "2021-03-26",
                        Time = "14:00:00Z"
                    },
                    ThirdPractice = new SessionDTO {
                        Date = "2021-03-27",
                        Time = "11:00:00Z"
                    },
                    Qualifying = new SessionDTO {
                        Date = "2021-03-27",
                        Time = "14:00:00Z"
                    },
                    Sprint = new SessionDTO {
                        Date = "2021-03-27",
                        Time = "16:30:00Z"
                    }
                },
                new RaceWeekendDTO {
                    Date = "2021-04-18",
                    Time = "14:00:00Z",
                    FirstPractice = new SessionDTO {
                        Date = "2021-04-16",
                        Time = "10:30:00Z"
                    },
                    SecondPractice = new SessionDTO {
                        Date = "2021-04-16",
                        Time = "14:00:00Z"
                    },
                    ThirdPractice = new SessionDTO {
                        Date = "2021-04-17",
                        Time = "11:00:00Z"
                    },
                    Qualifying = new SessionDTO {
                        Date = "2021-04-17",
                        Time = "14:00:00Z"
                    }
                }
            };


            var scheduleServiceMock = new Mock<IScheduleService>();
            var scheduleController = new ScheduleController(scheduleServiceMock.Object);

            scheduleServiceMock.Setup(s => s.GetFullSchedule())
                .ReturnsAsync(Events);

            List<RaceWeekendDTO>? result = scheduleController.GetSchedule().Result;
            Assert.That(result, Is.EqualTo(Events));
        }

        [Test]
        public void GetNextEvent_ShouldReturnNextEvent()
        {
            var Event = new RaceWeekendDTO
            {
                Date = "2021-03-28",
                Time = "15:00:00Z",
                FirstPractice = new SessionDTO
                {
                    Date = "2021-03-26",
                    Time = "10:30:00Z"
                },
                SecondPractice = new SessionDTO
                {
                    Date = "2021-03-26",
                    Time = "14:00:00Z"
                },
                ThirdPractice = new SessionDTO
                {
                    Date = "2021-03-27",
                    Time = "11:00:00Z"
                },
                Qualifying = new SessionDTO
                {
                    Date = "2021-03-27",
                    Time = "14:00:00Z"
                },
                Sprint = new SessionDTO
                {
                    Date = "2021-03-27",
                    Time = "16:30:00Z"
                }
            };


            var scheduleServiceMock = new Mock<IScheduleService>();
            var scheduleController = new ScheduleController(scheduleServiceMock.Object);

            scheduleServiceMock.Setup(s => s.GetNextOrCurrentEvent())
                .ReturnsAsync(Event);

            RaceWeekendDTO? result = scheduleController.GetNextEvent().Result;
            Assert.That(result, Is.EqualTo(Event));
        }
    }
}

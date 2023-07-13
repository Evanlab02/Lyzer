using Lyzer_BE.API.Controllers;
using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Moq;

namespace Lyzer_BE.Tests.API.Controllers
{
    [TestFixture]
    public class TestHydrationController
    {
        [Test]
        public async Task hydrateSchedule_ShouldReturnEntireSchedule()
        {
            var schedule = new ScheduleDTO()
            {
                ScheduleData = new ScheduleDataDTO()
                {
                    ScheduleTable = new ScheduleTableDTO()
                    {
                        RaceWeekends = new List<RaceWeekendDTO> {
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
                        }
                    }
                }
            };



            // Arrange
            var hydrationServiceMock = new Mock<IHydrationService>();
            var hydrationController = new HydrationController(hydrationServiceMock.Object);
            var currentYear = DateTime.Now.Year.ToString();
            hydrationServiceMock.Setup(service => service.HydrateSchedule(It.Is<string>(year => year == currentYear)))
                .Returns(Task.FromResult<ScheduleDTO>(schedule));

            ScheduleDTO result = await hydrationController.HydrateCurrentSchedule();
            Assert.That(result.ScheduleData.ScheduleTable.RaceWeekends.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task hydrateNextSchedule_ShouldReturnEntireSchedule()
        {
            var schedule = new ScheduleDTO()
            {
                ScheduleData = new ScheduleDataDTO()
                {
                    ScheduleTable = new ScheduleTableDTO()
                    {
                        RaceWeekends = new List<RaceWeekendDTO> {
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
                        }
                    }
                }
            };



            // Arrange
            var hydrationServiceMock = new Mock<IHydrationService>();
            var hydrationController = new HydrationController(hydrationServiceMock.Object);
            var yearFromNow = DateTime.Now.AddYears(1).Year.ToString();
            hydrationServiceMock.Setup(service => service.HydrateSchedule(It.Is<string>(year => year == yearFromNow)))
                .Returns(Task.FromResult<ScheduleDTO>(schedule));

            ScheduleDTO result = await hydrationController.HydrateFollowingYearSchedule();
            Assert.That(result.ScheduleData.ScheduleTable.RaceWeekends.Count, Is.EqualTo(2));
        }
    }
}

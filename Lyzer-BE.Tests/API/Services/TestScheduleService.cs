﻿using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Concrete;
using Lyzer_BE.API.Services.Interfaces;

namespace Lyzer_BE.Tests.API.Services
{
    [TestFixture]
    public class TestScheduleService
    {
        [Test]
        public void GetFullSchedule_ShouldReturn22Events()
        {
            IScheduleService scheduleService = new ScheduleService();
            List<EventDTO> events = scheduleService.GetFullSchedule().Result;
            Assert.That(events.Count, Is.EqualTo(22));
        }

        [Test]
        public void GetNextOrCurrentEvent()
        {
            var today = DateTime.Now;
            IScheduleService scheduleService = new ScheduleService();
            EventDTO events = scheduleService.GetNextOrCurrentEvent().Result;
            var endDateTime = DateTime.Parse(events.Date);
            Assert.That(today, Is.LessThan(endDateTime));
        }
    }
}
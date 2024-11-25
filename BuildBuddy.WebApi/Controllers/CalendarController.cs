﻿using BuildBuddy.Application.Abstractions;
using BuildBuddy.Contract;
using Microsoft.AspNetCore.Mvc;

namespace BuildBuddy.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalendarDto>>> GetAllCalendars()
        {
            var calendars = await _calendarService.GetAllCalendarsAsync();
            return Ok(calendars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CalendarDto>> GetCalendarById(int id)
        {
            var calendar = await _calendarService.GetCalendarByIdAsync(id);
            if (calendar == null)
            {
                return NotFound();
            }
            return Ok(calendar);
        }

        [HttpPost]
        public async Task<ActionResult<CalendarDto>> CreateCalendar(CalendarDto calendarDto)
        {
            var createdCalendar = await _calendarService.CreateCalendarAsync(calendarDto);
            return CreatedAtAction(nameof(GetCalendarById), new { id = createdCalendar.Id }, createdCalendar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCalendar(int id, CalendarDto calendarDto)
        {
            await _calendarService.UpdateCalendarAsync(id, calendarDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendar(int id)
        {
            await _calendarService.DeleteCalendarAsync(id);
            return NoContent();
        }

        [HttpPost("{calendarId}/tasks/{taskId}")]
        public async Task<IActionResult> AddTaskToCalendar(int calendarId, int taskId)
        {
            await _calendarService.AddTaskToCalendarAsync(calendarId, taskId);
            return NoContent();
        }

        [HttpDelete("{calendarId}/tasks/{taskId}")]
        public async Task<IActionResult> RemoveTaskFromCalendar(int calendarId, int taskId)
        {
            await _calendarService.RemoveTaskFromCalendarAsync(calendarId, taskId);
            return NoContent();
        }

        [HttpGet("getByUserId/{userId}")]
        public async Task<IActionResult> GetCalendarByUserId(int userId)
        {
            var calendars = await _calendarService.GetCalendarByUserIdAsync(userId);

            if (calendars == null || !calendars.Any())
            {
                return NotFound("No calendars found for the given user.");
            }

            return Ok(calendars);
        }

    }
}

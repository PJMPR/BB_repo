using BuildBuddy.Data.Abstractions;
using BuildBuddy.Data.Model;
using BuildBuddy.Contract;
using BuildBuddy.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BuildBuddy.Application.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IRepositoryCatalog _repository;

        public CalendarService(IRepositoryCatalog repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CalendarDto>> GetAllCalendarsAsync()
        {
            return await _repository.Calendars
                .Entities
                .Select(c => new CalendarDto
                {
                    Name = c.Name,
                    Description = c.Description,
                    Timezone = c.Timezone,
                    UserId = c.UserId
                })
                .ToListAsync();
        }

        public async Task<CalendarDto> GetCalendarByIdAsync(int id)
        {
            var calendar = await _repository.Calendars
                .Entities
                .FirstOrDefaultAsync(c => c.Id == id);

            if (calendar == null)
            {
                return null;
            }

            return new CalendarDto
            {
                Name = calendar.Name,
                Description = calendar.Description,
                Timezone = calendar.Timezone,
                UserId = calendar.UserId
            };
        }

        public async Task<CalendarDto> CreateCalendarAsync(CalendarDto calendarDto)
        {
            var calendar = new Calendar
            {
                Name = calendarDto.Name,
                Description = calendarDto.Description,
                Timezone = calendarDto.Timezone,
                UserId = calendarDto.UserId
            };

            _repository.Calendars.Add(calendar);
            await _repository.SaveChangesAsync();

            calendarDto.Id = calendar.Id;

            return calendarDto;
        }

        public async Task UpdateCalendarAsync(int id, CalendarDto calendarDto)
        {
            var calendar = await _repository.Calendars.Entities.FirstOrDefaultAsync(c => c.Id == id);

            if (calendar != null)
            {
                calendar.Name = calendarDto.Name;
                calendar.Description = calendarDto.Description;
                calendar.Timezone = calendarDto.Timezone;
                calendar.UserId = calendarDto.UserId;

                await _repository.SaveChangesAsync();
            }
        }

        public async Task DeleteCalendarAsync(int id)
        {
            var calendar = await _repository.Calendars.Entities.FirstOrDefaultAsync(c => c.Id == id);
            if (calendar != null)
            {
                _repository.Calendars.Remove(calendar);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task AddTaskToCalendarAsync(int calendarId, int taskId)
        {
            var calendar = await _repository.Calendars
                .Entities
                .Include(c => c.CalendarTasks)
                .FirstOrDefaultAsync(c => c.Id == calendarId);

            var task = await _repository.Tasks.Entities.FirstOrDefaultAsync(x => x.Id == taskId);

            if (calendar != null && task != null && !calendar.CalendarTasks.Any(ct => ct.TaskId == taskId))
            {
                calendar.CalendarTasks.Add(new CalendarTask { CalendarId = calendarId, TaskId = taskId, Calendar = calendar, Tasks = task });
                await _repository.SaveChangesAsync();
            }
        }
        public async Task RemoveTaskFromCalendarAsync(int calendarId, int taskId)
        {
            var calendarTask = await _repository.CalendarTasks
                .Entities
                .FirstOrDefaultAsync(ct => ct.CalendarId == calendarId && ct.TaskId == taskId);

            if (calendarTask != null)
            {
                _repository.CalendarTasks.Remove(calendarTask);
                await _repository.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<CalendarDto>> GetCalendarByUserIdAsync(int userId)
        {
            return await _repository.Calendars
                .Entities
                .Where(c => c.UserId == userId)
                .Select(c => new CalendarDto
                {
                    Name = c.Name,
                    Description = c.Description,
                    Timezone = c.Timezone,
                    UserId = c.UserId
                })
                .ToListAsync();
        }

    }
}

using BuildBuddy.Application.Abstractions;
using BuildBuddy.Contract;
using BuildBuddy.Data.Abstractions;
using BuildBuddy.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace BuildBuddy.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepositoryCatalog _repositories;

        public TaskService(IRepositoryCatalog repositories)
        {
            _repositories = repositories;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            return await _repositories.Tasks.Entities
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Message = t.Message,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    AllDay = t.AllDay,
                    PlaceId = t.PlaceId ?? 0
                })
                .ToListAsync();
        }

        public async Task<TaskDto> GetTaskIdAsync(int id)
        {
            var task = await _repositories.Tasks.Entities
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                return null;
            }

            return new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Message = task.Message,
                StartTime = task.StartTime,
                EndTime = task.EndTime,
                AllDay = task.AllDay,
                PlaceId = task.PlaceId ?? 0
            };
        }

        public async Task<TaskDto> CreateTaskAsync(TaskDto taskDto)
        {
            var task = new Tasks
            {
                Name = taskDto.Name,
                Message = taskDto.Message,
                StartTime = taskDto.StartTime,
                EndTime = taskDto.EndTime,
                AllDay = taskDto.AllDay,
                PlaceId = taskDto.PlaceId
            };

            _repositories.Tasks.Add(task);
            await _repositories.SaveChangesAsync();

            taskDto.Id = task.Id;
            return taskDto;
        }

        public async Task UpdateTaskAsync(int id, TaskDto taskDto)
        {
            var task = await _repositories.Tasks.Entities.FirstOrDefaultAsync(t => t.Id == id);

            if (task != null)
            {
                task.Name = taskDto.Name;
                task.Message = taskDto.Message;
                task.StartTime = taskDto.StartTime;
                task.EndTime = taskDto.EndTime;
                task.AllDay = taskDto.AllDay;
                task.PlaceId = taskDto.PlaceId;

                await _repositories.SaveChangesAsync();
            }
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _repositories.Tasks.Entities.FirstOrDefaultAsync(t => t.Id == id);
            if (task != null)
            {
                _repositories.Tasks.Remove(task);
                await _repositories.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksByCalendarIdAsync(int calendarId)
        {
            return await _repositories.CalendarTasks.Entities
                .Where(ct => ct.CalendarId == calendarId)
                .Select(ct => new TaskDto
                {
                    Id = ct.Tasks.Id,
                    Name = ct.Tasks.Name,
                    Message = ct.Tasks.Message,
                    StartTime = ct.Tasks.StartTime,
                    EndTime = ct.Tasks.EndTime,
                    AllDay = ct.Tasks.AllDay,
                    PlaceId = ct.Tasks.PlaceId ?? 0
                })
                .ToListAsync();
        }
    }
}

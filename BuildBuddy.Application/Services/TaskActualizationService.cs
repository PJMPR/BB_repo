using BuildBuddy.Application.Abstractions;
using BuildBuddy.Contract;
using BuildBuddy.Data.Abstractions;
using BuildBuddy.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace BuildBuddy.Application.Services
{
    public class TaskActualizationService : ITaskActualizationService
    {
        private readonly IRepositoryCatalog _repositories;

        public TaskActualizationService(IRepositoryCatalog repositories)
        {
            _repositories = repositories;
        }

        public async Task<IEnumerable<TaskActualizationDto>> GetAllTasksActualizationAsync()
        {
            return await _repositories.TaskActualizations
                .Entities
                .Select(ta => new TaskActualizationDto
                {
                    Id = ta.Id,
                    Message = ta.Message,
                    IsDone = ta.IsDone,
                    TaskImageUrl = ta.TaskImageUrl,
                    TaskId = ta.TaskId
                })
                .ToListAsync();
        }

        public async Task<TaskActualizationDto> GetTaskActualizationByIdAsync(int id)
        {
            var taskActualization = await _repositories.TaskActualizations
                .Entities
                .FirstOrDefaultAsync(ta => ta.Id == id);

            if (taskActualization == null)
            {
                return null;
            }

            return new TaskActualizationDto
            {
                Id = taskActualization.Id,
                Message = taskActualization.Message,
                IsDone = taskActualization.IsDone,
                TaskImageUrl = taskActualization.TaskImageUrl,
                TaskId = taskActualization.TaskId
            };
        }

        public async Task<TaskActualizationDto> CreateTaskActualizationAsync(TaskActualizationDto taskActualizationDto)
        {
            var taskActualization = new TaskActualization
            {
                Message = taskActualizationDto.Message,
                IsDone = taskActualizationDto.IsDone,
                TaskImageUrl = taskActualizationDto.TaskImageUrl,
                TaskId = taskActualizationDto.TaskId
            };

            _repositories.TaskActualizations.Add(taskActualization);
            await _repositories.SaveChangesAsync();

            taskActualizationDto.Id = taskActualization.Id;
            return taskActualizationDto;
        }

        public async Task UpdateTaskActualizationAsync(int id, TaskActualizationDto taskActualizationDto)
        {
            var taskActualization = await _repositories.TaskActualizations
                .Entities
                .FirstOrDefaultAsync(ta => ta.Id == id);

            if (taskActualization != null)
            {
                taskActualization.Message = taskActualizationDto.Message;
                taskActualization.IsDone = taskActualizationDto.IsDone;
                taskActualization.TaskImageUrl = taskActualizationDto.TaskImageUrl;
                taskActualization.TaskId = taskActualizationDto.TaskId;

                await _repositories.SaveChangesAsync();
            }
        }

        public async Task DeleteTaskActualizationAsync(int id)
        {
            var taskActualization = await _repositories.TaskActualizations
                .Entities
                .FirstOrDefaultAsync(ta => ta.Id == id);

            if (taskActualization != null)
            {
                _repositories.TaskActualizations.Remove(taskActualization);
                await _repositories.SaveChangesAsync();
            }
        }
    }
}

using BuildBuddy.Application.Abstractions;
using BuildBuddy.Application.Services;
using Microsoft.Extensions.DependencyInjection;


namespace BuildBuddy.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBuildBuddyApp(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>()
                .AddScoped<ICalendarService, CalendarService>()
                .AddScoped<ITaskActualizationService, TaskActualizationService>()
                .AddScoped<ITaskService, TaskService>()
                .AddScoped<IPlaceService, PlaceService>()
                .AddScoped<ITeamService, TeamService>()
                .AddScoped<IUserService, UserService>();
            return services;
        }

    }
}

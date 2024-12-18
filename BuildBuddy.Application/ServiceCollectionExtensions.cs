﻿using BuildBuddy.Application.Abstractions;
using BuildBuddy.Application.Services;
using BuildBuddy.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace BuildBuddy.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBuildBuddyApp(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IItemService, ItemService>()
                .AddScoped<ICalendarService, CalendarService>()
                .AddScoped<ITaskActualizationService, TaskActualizationService>()
                .AddScoped<ITaskService, TaskService>()
                .AddScoped<IPlaceService, PlaceService>()
                .AddScoped<ITeamService, TeamService>()
                .AddScoped<IUserService, UserService>()
                .AddBuildBuddyData(connectionString);
            return services;
        }

    }
}

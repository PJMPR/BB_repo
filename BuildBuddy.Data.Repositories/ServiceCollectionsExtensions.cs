﻿using BuildBuddy.Data.Abstractions;
using BuildBuddy.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildBuddy.Data.Repositories
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddBuildBuddyData(this IServiceCollection services, string connectionString) 
        {
            return services.AddDbContext<BuildBuddyDbContext>(options =>
                options.UseNpgsql(connectionString))
                .AddScoped<IRepository<User, int>, MainRepository<User, int>>()
                .AddScoped<IRepository<Calendar, int>, MainRepository<Calendar, int>>()
                .AddScoped<IRepository<CalendarTask, int>, MainRepository<CalendarTask, int>>()
                .AddScoped<IRepository<Conversation, int>, MainRepository<Conversation, int>>()
                .AddScoped<IRepository<Item, int>, MainRepository<Item, int>>()
                .AddScoped<IRepository<Message, int>, MainRepository<Message, int>>()
                .AddScoped<IRepository<Place, int>, MainRepository<Place, int>>()
                .AddScoped<IRepository<TaskActualization, int>, MainRepository<TaskActualization, int>>()
                .AddScoped<IRepository<Tasks, int>, MainRepository<Tasks, int>>()
                .AddScoped<IRepository<Team, int>, MainRepository<Team, int>>()
                .AddScoped<IRepository<TeamUser, int>, MainRepository<TeamUser, int>>()
                .AddScoped<IRepository<User, int>, MainRepository<User, int>>()
                .AddScoped<IRepository<UserConversation, int>, MainRepository<UserConversation, int>>()
                .AddScoped<IRepositoryCatalog, UnitOfWork>();
        }
    }
}

using BuildBuddy.Data.Abstractions;
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
                .AddScoped<IRepository<User, int>, GenericRepository<User, int>>()
                .AddScoped<IRepository<Calendar, int>, GenericRepository<Calendar, int>>()
                .AddScoped<IRepository<CalendarTask, int>, GenericRepository<CalendarTask, int>>()
                .AddScoped<IRepository<Conversation, int>, GenericRepository<Conversation, int>>()
                .AddScoped<IRepository<Item, int>, GenericRepository<Item, int>>()
                .AddScoped<IRepository<Message, int>, GenericRepository<Message, int>>()
                .AddScoped<IRepository<Place, int>, GenericRepository<Place, int>>()
                .AddScoped<IRepository<TaskActualization, int>, GenericRepository<TaskActualization, int>>()
                .AddScoped<IRepository<Tasks, int>, GenericRepository<Tasks, int>>()
                .AddScoped<IRepository<Team, int>, GenericRepository<Team, int>>()
                .AddScoped<IRepository<TeamUser, int>, GenericRepository<TeamUser, int>>()
                .AddScoped<IRepository<User, int>, GenericRepository<User, int>>()
                .AddScoped<IRepository<UserConversation, int>, GenericRepository<UserConversation, int>>()
                .AddScoped<IRepositoryCatalog, GenericRepositoryCatalog>();
        }
    }
}

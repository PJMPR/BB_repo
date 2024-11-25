using BuildBuddy.Data.Abstractions;
using BuildBuddy.Data.Model;

namespace BuildBuddy.Data.Repositories
{
    internal class GenericRepositoryCatalog : IRepositoryCatalog
    {
        private readonly BuildBuddyDbContext _ctx;

        public GenericRepositoryCatalog(BuildBuddyDbContext ctx,
            IRepository<User, int> users,
            IRepository<Team, int> teams,
            IRepository<Conversation, int> convs,
            IRepository<UserConversation, int> usersConv,
            IRepository<Calendar, int> calendars,
            IRepository<CalendarTask, int> calendartasks,
            IRepository<Tasks, int> tasks,
            IRepository<TaskActualization, int> tasksActualizations,
            IRepository<Item, int> items,
            IRepository<Place, int> places,
            IRepository<Message, int> messages
            )
        {
            Users=users;
            Teams=teams;
            Conversations=convs;
            UserConversations=usersConv;
            Calendars=calendars;
            CalendarTasks = calendartasks;
            Tasks=tasks;
            TaskActualizations=tasksActualizations;
            Items=items;
            Places=places;
            Messages=messages;
            _ctx = ctx;
        }

        public IRepository<User, int> Users {get;}

        public IRepository<Team, int> Teams {get;}

        public IRepository<Conversation, int> Conversations {get;}

        public IRepository<UserConversation, int> UserConversations {get;}

        public IRepository<Calendar, int> Calendars {get;}

        public IRepository<CalendarTask, int> CalendarTasks {get;}

        public IRepository<Tasks, int> Tasks {get;}

        public IRepository<TaskActualization, int> TaskActualizations {get;}

        public IRepository<Item, int> Items {get;}

        public IRepository<Place, int> Places {get;}

        public IRepository<Message, int> Messages {get;}

        public Task SaveChangesAsync()
        {
            return _ctx.SaveChangesAsync();
        }
    }
}

using BuildBuddy.Data.Model;

namespace BuildBuddy.Data.Abstractions
{
    public interface IRepositoryCatalog
    {
        public IRepository<User, int> Users { get; }
        public IRepository<Team, int> Teams { get; }
        public IRepository<Conversation, int> Conversations { get; }
        public IRepository<UserConversation, int> UserConversations { get; }
        public IRepository<Calendar, int> Calendars { get; }
        public IRepository<CalendarTask, int> CalendarTasks { get; }
        public IRepository<Tasks, int> Tasks { get; }
        public IRepository<TaskActualization, int> TaskActualizations { get; }
        public IRepository<Item, int> Items { get; }
        public IRepository<Place, int> Places { get; }
        public IRepository<Message, int> Messages { get; }

        public Task SaveChangesAsync();
    }
}

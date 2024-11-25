namespace BuildBuddy.Data.Model;

public class Calendar : IHaveId<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TimeSpan Timezone { get; set; }
    public int UserId { get; set; }
    public virtual ICollection<CalendarTask> CalendarTasks { get; set; }
}
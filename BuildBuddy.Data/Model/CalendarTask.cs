﻿namespace BuildBuddy.Data.Model;

public class CalendarTask : IHaveId<int>
{
    public int Id { get; set; }
    public int CalendarId { get; set; }
    public int TaskId { get; set; }

    public virtual Calendar Calendar { get; set; }
    public virtual Tasks Tasks { get; set; }
}
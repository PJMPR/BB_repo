﻿namespace Backend.Model;

public class TaskActualization
{
    public int Id { get; set; }
    public int ImageId { get; set; }
    public int Message { get; set; }
    public bool IsDone { get; set; }
    public List<string> TaskImageUrl { get; set; }

    public virtual Task Task { get; set; }
}
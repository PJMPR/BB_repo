﻿namespace Backend.Dto;

public class TaskActualizationDto
{
    public int ImageId { get; set; }
    public int Message { get; set; }
    public bool IsDone { get; set; }
    public List<string> TaskImageUrl { get; set; }
    
}
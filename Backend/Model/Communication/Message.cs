﻿namespace Backend.Model;

public class Message
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public string Text { get; set; }
    public DateTime DateTimeDate { get; set; }
    public int ConversationId { get; set; }

    public virtual User Sender { get; set; }
    public virtual Conversation Conversation { get; set; }
}
﻿namespace BuildBuddy.Data.Model;

public class User : IHaveId<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Mail { get; set; }
    public string TelephoneNr { get; set; }
    public string Password { get; set; }
    public string UserImageUrl { get; set; }
    public int TeamId { get; set; }

    public virtual ICollection<TeamUser> TeamUsers { get; set; }
    public virtual Calendar Calendars { get; set; }
    public virtual ICollection<UserConversation> UserConversations { get; set; }
    public virtual ICollection<Message> Message { get; set; }

}
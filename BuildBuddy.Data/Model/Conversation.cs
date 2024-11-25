
namespace BuildBuddy.Data.Model;

public class Conversation : IHaveId<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TeamId { get; set; }

    public virtual ICollection<Message> Messages { get; set; }
    public virtual ICollection<UserConversation> UserConversations { get; set; }
    public virtual Team Team { get; set; }
}
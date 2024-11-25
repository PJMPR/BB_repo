
namespace BuildBuddy.Application.Abstractions;

public interface IConversationService
{
    Task AddUserToConversationAsync(int conversationId, int userId);
    Task RemoveUserFromConversationAsync(int conversationId, int userId);
}
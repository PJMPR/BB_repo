using BuildBuddy.Application.Abstractions;

namespace BuildBuddy.Application.Services;

public class ConversationService : IConversationService
{
    public Task AddUserToConversationAsync(int conversationId, int userId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveUserFromConversationAsync(int conversationId, int userId)
    {
        throw new NotImplementedException();
    }
}
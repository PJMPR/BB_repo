﻿using BuildBuddy.Contract;

namespace BuildBuddy.Application.Abstractions;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(int userId);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> CreateUserAsync(UserDto userDto);
    Task UpdateUserAsync(int userId, UserDto userDto);
    Task DeleteUserAsync(int userId);
    Task<IEnumerable<ConversationDto>> GetUserConversationsAsync(int userId);
    Task<IEnumerable<TeamDto>> GetUserTeamsAsync(int userId);
    string GenerateJwtToken(UserDto user);
}
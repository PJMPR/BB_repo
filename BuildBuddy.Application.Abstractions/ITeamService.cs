﻿
using BuildBuddy.Contract;

namespace BuildBuddy.Application.Abstractions;

public interface ITeamService
{
    Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
    Task<List<TeamDto>> GetTeamsByUserId(int userId);
    Task<TeamDto> GetTeamByIdAsync(int id);
    Task<TeamDto> CreateTeamAsync(TeamDto conversationDto);
    Task UpdateTeamAsync(int id, TeamDto conversationDto);
    Task DeleteTeamAsync(int id);
    Task AddUserToTeamAsync(int teamId, int userId);
    Task RemoveUserFromTeamAsync(int teamId, int userId);
}
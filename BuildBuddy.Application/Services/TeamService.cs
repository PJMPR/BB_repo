using BuildBuddy.Application.Abstractions;
using BuildBuddy.Contract;
using BuildBuddy.Data.Abstractions;
using BuildBuddy.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace BuildBuddy.Application.Services
{

    public class TeamService : ITeamService
    {
        private readonly IRepositoryCatalog _repositories;

        public TeamService(IRepositoryCatalog dbContext)
        {
            _repositories = dbContext;
        }

        public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
        {
            return await _repositories.Teams
                .Entities
                .Select(team => new TeamDto
                {
                    Id = team.Id,
                    Name = team.Name,
                    PlaceId = team.PlaceId
                })
                .ToListAsync();
        }

        public async Task<TeamDto> GetTeamByIdAsync(int id)
        {
            var team = await _repositories.Teams.Entities
                .FirstOrDefaultAsync(t => t.Id == id);

            if (team == null)
            {
                return null;
            }

            return new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                PlaceId = team.PlaceId
            };
        }

        public async Task<TeamDto> CreateTeamAsync(TeamDto teamDto)
        {
            var team = new Team
            {
                Name = teamDto.Name,
                PlaceId = teamDto.PlaceId
            };

            _repositories.Teams.Add(team);
            await _repositories.SaveChangesAsync();

            teamDto.Id = team.Id;
            return teamDto;
        }

        public async Task UpdateTeamAsync(int id, TeamDto teamDto)
        {
            var team = await _repositories.Teams.Entities.FirstOrDefaultAsync(t => t.Id == id);

            if (team != null)
            {
                team.Name = teamDto.Name;
                team.PlaceId = teamDto.PlaceId;

                await _repositories.SaveChangesAsync();
            }
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _repositories.Teams.Entities.FirstOrDefaultAsync(t => t.Id == id);
            if (team != null)
            {
                _repositories.Teams.Remove(team);
                await _repositories.SaveChangesAsync();
            }
        }

        public async Task AddUserToTeamAsync(int teamId, int userId)
        {
            var team = await _repositories.Teams.Entities
                .Include(t => t.TeamUsers)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            if (team != null && !team.TeamUsers.Any(tu => tu.UserId == userId))
            {
                team.TeamUsers.Add(new TeamUser
                {
                    TeamId = teamId,
                    UserId = userId
                });

                await _repositories.SaveChangesAsync();
            }
        }

        public async Task RemoveUserFromTeamAsync(int teamId, int userId)
        {
            var team = await _repositories.Teams.Entities
                .Include(t => t.TeamUsers)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            var teamUser = team?.TeamUsers.FirstOrDefault(tu => tu.UserId == userId);
            if (teamUser != null)
            {
                team.TeamUsers.Remove(teamUser);
                await _repositories.SaveChangesAsync();
            }
        }

        public async Task<List<TeamDto>> GetTeamsByUserId(int userId)
        {
            var userExists = await _repositories.Users.Entities.AnyAsync(u => u.Id == userId);
            if (!userExists)
            {
                return null;
            }
            var teams = await _repositories.Teams.Entities
                .Where(t => t.TeamUsers.Any(tu => tu.UserId == userId))
                .Select(t => new TeamDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    PlaceId = t.PlaceId,
                })
                .ToListAsync();

            return teams;
        }
    }
}

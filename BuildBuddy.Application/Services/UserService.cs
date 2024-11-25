using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using BuildBuddy.Data.Abstractions;
using BuildBuddy.Data.Model;
using BuildBuddy.Contract;
using BuildBuddy.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace BuildBuddy.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositoryCatalog _repositories;

        public UserService(IRepositoryCatalog dbContext, IConfiguration configuration)
        {
            _repositories = dbContext;
            _configuration = configuration;
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _repositories.Users.Entities
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Mail = user.Mail,
                TelephoneNr = user.TelephoneNr,
                UserImageUrl = user.UserImageUrl,
                TeamId = user.TeamId
            };
        }
        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _repositories.Users.Entities.FirstOrDefaultAsync(u => u.Mail == email);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Mail = user.Mail,
                TelephoneNr = user.TelephoneNr,
                Password = user.Password,
                UserImageUrl = user.UserImageUrl,
                TeamId = user.TeamId
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _repositories.Users.Entities
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Mail = user.Mail,
                    Surname = user.Surname,
                    Password = user.Password,
                    TelephoneNr = user.TelephoneNr,
                    UserImageUrl = user.UserImageUrl,
                    TeamId = user.TeamId
                })
                .ToListAsync();
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Surname = userDto.Surname,
                TelephoneNr = userDto.TelephoneNr,
                Mail = userDto.Mail,
                Password = userDto.Password,
                UserImageUrl = userDto.UserImageUrl,
                TeamId = userDto.TeamId
            };

            _repositories.Users.Add(user);
            await _repositories.SaveChangesAsync();

            userDto.Id = user.Id;
            return userDto;
        }

        public async Task UpdateUserAsync(int userId, UserDto userDto)
        {
            var user = await _repositories.Users.Entities.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                user.Name = userDto.Name;
                user.Surname = userDto.Surname;
                user.TelephoneNr = userDto.TelephoneNr;
                user.Mail = userDto.Mail;
                user.Password = userDto.Password;
                user.UserImageUrl = userDto.UserImageUrl;
                user.TeamId = userDto.TeamId;

                await _repositories.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _repositories.Users.Entities.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                _repositories.Users.Remove(user);
                await _repositories.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ConversationDto>> GetUserConversationsAsync(int userId)
        {
            return await _repositories.UserConversations.Entities
                .Where(uc => uc.UserId == userId)
                .Select(uc => new ConversationDto
                {
                    Id = uc.Conversation.Id,
                    Name = uc.Conversation.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TeamDto>> GetUserTeamsAsync(int userId)
        {
            var user = await _repositories.Users.Entities
                .Include(u => u.TeamUsers)
                .ThenInclude(tu => tu.Team)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return null;
            }

            return user.TeamUsers.Select(tu => new TeamDto
            {
                Id = tu.Team.Id,
                Name = tu.Team.Name,
            });
        }
        public string GenerateJwtToken(UserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? string.Empty);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("mail", user.Mail)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

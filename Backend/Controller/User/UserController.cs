﻿using Backend.Dto;
using Backend.Interface.User;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto userDto)
        {
            var createdUser = await _userService.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            await _userService.UpdateUserAsync(id, userDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/conversations")]
        public async Task<ActionResult<IEnumerable<ConversationDto>>> GetUserConversations(int id)
        {
            var conversations = await _userService.GetUserConversationsAsync(id);
            return Ok(conversations);
        }

        [HttpGet("{id}/teams")]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetUserTeams(int id)
        {
            var teams = await _userService.GetUserTeamsAsync(id);
            return Ok(teams);
        }
    }
}

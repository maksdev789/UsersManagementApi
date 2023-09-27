using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using UsersManagement.Core;
using UsersManagement.Domain.Models;

namespace UsersManagement.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(UserModelCreate userModelCreate) 
        {
            var result = await _userService.AddUserAsync(userModelCreate);

            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserAsync(int userId)
        {
            var result = await _userService.GetUserAsync(userId);

            if (result == null) 
            {
                return NotFound();                 
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _userService.GetAllUsersAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("page")]
        public async Task<IActionResult> GetUsersPageAsync(int skip,int take, string searchText)
        {
            var result = await _userService.GetUsersPageAsync(skip, take, searchText);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync(UserModel userModel)
        {
            var result = await _userService.UpdateUserAsync(userModel);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            var result = await _userService.DeleteUserAsync(userId);

            return Ok(result);
        }

        //public async Task<int> Sum(int x, int y)
        //{
        //    await Task.Yield();

        //    return x + y;
        //}

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Dto;
using Services.Users;

namespace AuthServerAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController(IUserService  userService) : CustomController
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto userDto)
        {
            var result = await userService.CreateUserAsync(userDto);
            return Result(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            Console.WriteLine("GetUser Name: " + HttpContext.User.Identity.Name);

            var result = await userService.GetUserByName(HttpContext.User.Identity.Name);
            return Result(result);
        }
    }
}

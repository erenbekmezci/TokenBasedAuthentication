using Azure;
using Microsoft.AspNetCore.Identity;
using Repositories.Users;
using Services.Dto;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ServiceResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new User { Email = createUserDto.email, UserName = createUserDto.userName };

            var result = await _userManager.CreateAsync(user, createUserDto.password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return ServiceResult<UserDto>.Fail(errors);
            }
            return ServiceResult<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(user));
        }

        public async Task<ServiceResult<UserDto>> GetUserByName(string userName)
        {
            Console.Write("sasdasdasd");
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return ServiceResult<UserDto>.Fail("UserName not found");
            }

            return ServiceResult<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(user));
        }
    }
}

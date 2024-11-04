using Services.Dto;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
    public interface IUserService
    {
        Task<ServiceResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto);

        Task<ServiceResult<UserDto>> GetUserByName(string userName);

    }
}

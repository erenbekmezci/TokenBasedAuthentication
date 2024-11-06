using Microsoft.AspNetCore.Mvc;
using Services.Authentication;
using Services.Dto;
using Shared;

namespace AuthServerAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class AuthController(IAuthenticationService authenticationService) : CustomController
    {

        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            Console.WriteLine("dfsdfsdf");
            var response = await authenticationService.CreateTokenAsync(loginDto);
            return Result(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshToken)
        {
            var response = await authenticationService.CreateTokenByRefreshToken(refreshToken.token);
            return Result(response);
        }
        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            ServiceResult<ClientTokenDto> result = authenticationService.CreateTokenByClientAsync(clientLoginDto);

            return Result(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            ServiceResult result = await authenticationService.RevokeRefreshToken(refreshTokenDto.token);

            return Result(result);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repositories;
using Repositories.UnitOfWorks;
using Repositories.UserRefreshTokens;
using Repositories.Users;
using Services.Configuration;
using Services.Dto;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Authentication
{
    public class AuthenticationService
    (
        UserManager<User> userManager,
        ITokenService tokenService,
        IGenericRepository<UserRefreshToken> userRefreshTokenService,
        IUnitOfWork unitOfWork,
        IOptions<List<Client>> clientsOption
    ) : IAuthenticationService
    {
        private readonly List<Client> clients = clientsOption.Value;
        public async Task<ServiceResult<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.email);

            if (user == null) return ServiceResult<TokenDto>.Fail("email or password not found");

            if (!await userManager.CheckPasswordAsync(user, loginDto.password))
                return ServiceResult<TokenDto>.Fail("email or password not found");

            var token = tokenService.CreateToken(user);

            var userRefreshToken = userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefault();

            if (userRefreshToken == null)
            {
                await userRefreshTokenService.AddAsync(new UserRefreshToken()
                {
                    Code = token.RefreshToken,
                    UserId = user.Id,
                    ExpireTime = token.RefreshTokenExpiration
                });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.ExpireTime = token.RefreshTokenExpiration;
            }
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<TokenDto>.Success(token);


        }

        public ServiceResult<ClientTokenDto> CreateTokenByClientAsync(ClientLoginDto clientDto)
        {
            var client = clients.SingleOrDefault(x => x.Id == clientDto.clientID && x.Secret == clientDto.secret);

            if (client == null) ServiceResult<ClientTokenDto>.Fail("client id or secret not found");

            var token = tokenService.CreateTokenByClient(client!);

            return ServiceResult<ClientTokenDto>.Success(token);


        }

        public async Task<ServiceResult<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefault();

            if (existRefreshToken == null) ServiceResult<TokenDto>.Fail("refresh token not found", System.Net.HttpStatusCode.NotFound);

            var user = await userManager.FindByIdAsync(existRefreshToken!.UserId);

            if (user == null) ServiceResult<TokenDto>.Fail("user not found", System.Net.HttpStatusCode.NotFound);

            var token = tokenService.CreateToken(user!);

            existRefreshToken.Code = token.RefreshToken;
            existRefreshToken.ExpireTime = token.RefreshTokenExpiration;
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<TokenDto>.Success(token);


        }

        public async Task<ServiceResult> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null) ServiceResult.Fail("refresh token not found", System.Net.HttpStatusCode.NotFound);

            userRefreshTokenService.Delete(existRefreshToken!);

            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success();
        }
    }
}

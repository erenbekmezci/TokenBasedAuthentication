using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repositories.Users;
using Services.Configuration;
using Services.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Services.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly CustomTokenOption tokenOption;

        public TokenService(IOptions<CustomTokenOption> option)
        {


            tokenOption = option.Value;
        }

        public IEnumerable<Claim> GetClaims(User user, List<string> audiences)
        {
            var userClamims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , user.UserName!),
                new Claim (ClaimTypes.Email , user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier , user.Id)


            };
            userClamims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return userClamims;
        }
        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();


            claims.AddRange(client.Audiences!.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString()));

            return claims;
        }


        public TokenDto CreateToken(User user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(tokenOption.RefreshTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(tokenOption.SecurityKey!);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            Console.WriteLine(signingCredentials);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(user, tokenOption.Audience!),
                signingCredentials: signingCredentials
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwtSecurityToken);
            Console.WriteLine(token);

            var tokenDto = new TokenDto(token, accessTokenExpiration, CreateRefreshToken(), refreshTokenExpiration);

            return tokenDto;
        }
        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using var random = RandomNumberGenerator.Create();

            random.GetBytes(number);
            Console.WriteLine(Convert.ToBase64String(number));
            return Convert.ToBase64String(number);

        }
        public ClientTokenDto CreateTokenByClient(Client client)
        {
            var accessTokenExp = DateTime.Now.AddMinutes(tokenOption.AccessTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(tokenOption.SecurityKey!);
            var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: tokenOption.Issuer,
                expires: accessTokenExp,
                claims: GetClaimsByClient(client),
                notBefore: DateTime.Now,
                signingCredentials: signinCredentials

            );
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwtSecurityToken);
            var clientTokenDto = new ClientTokenDto()
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExp,

            };
            return clientTokenDto;
        }
    }
}

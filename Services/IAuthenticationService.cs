using Services.Dto;
using Shared;


namespace Services
{
    public interface IAuthenticationService
    {
        Task<ServiceResult<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<ServiceResult<TokenDto>> CreateTokenByRefreshToken(string refreshToken);

        Task<ServiceResult> RevokeRefreshToken(string refreshToken);//tokeni null yapar kullanıcı logout olduğunda veya sürekli bir istek olduğunda(çalınma riski)

        Task<ServiceResult<ClientTokenDto>> CreateTokenByClientAsync(ClientLoginDto clientDto);


    }
}

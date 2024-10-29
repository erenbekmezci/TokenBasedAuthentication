using Services.Dto;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

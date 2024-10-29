using Repositories;
using Services.Configuration;
using Services.Dto;

namespace Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(User user);
        ClientTokenDto CreateTokenByClient(Client client);

    }
}

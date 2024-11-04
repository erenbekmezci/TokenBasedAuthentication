using Repositories.Users;
using Services.Configuration;
using Services.Dto;

namespace Services.Authentication
{
    public interface ITokenService
    {
        TokenDto CreateToken(User user);
        ClientTokenDto CreateTokenByClient(Client client);

    }
}



namespace Repositories.UserRefreshTokens
{
    public class UserRefreshToken
    {
        public string UserId { get; set; } = default!;
        public string Code { get; set; } = default!;
        public DateTime ExpireTime { get; set; }
    }

}

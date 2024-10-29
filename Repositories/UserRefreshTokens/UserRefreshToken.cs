

namespace Repositories.UserRefreshTokens
{
    public class UserRefreshToken
    {
        public string UserId { get; set; } = default!;
        public int Code { get; set; }
        public DateTime ExpireTime { get; set; }
    }

}

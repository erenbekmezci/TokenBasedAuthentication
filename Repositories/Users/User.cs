using Microsoft.AspNetCore.Identity;


namespace Repositories.Users
{
    public class User : IdentityUser
    {
        public string? City { get; set; }
    }
}

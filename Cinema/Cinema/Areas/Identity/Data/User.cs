using Microsoft.AspNetCore.Identity;

namespace Cinema.Areas.Identity
{
    public class User : IdentityUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

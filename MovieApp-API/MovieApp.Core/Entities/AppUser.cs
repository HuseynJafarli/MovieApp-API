using Microsoft.AspNetCore.Identity;

namespace MovieApp.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace ApiPractice.Data.Entities
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}

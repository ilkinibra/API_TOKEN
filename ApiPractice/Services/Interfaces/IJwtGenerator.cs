using ApiPractice.Data.Entities;
using System.Collections.Generic;

namespace ApiPractice.Services.Interfaces
{
    public interface IJwtGenerator
    {
        string GenerateJwt(AppUser user, IList<string> roles);
    }
}

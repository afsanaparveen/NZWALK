using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.repositories
{
    public interface ITokenRepository
    {
       string CreateJWTToken(IdentityUser user,List<string> roles);
    }
}

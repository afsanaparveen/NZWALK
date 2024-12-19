using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.repositories
{
    public class TokenRepository:ITokenRepository
    {
        private readonly IConfiguration configuration;
        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            //create claims from the roles and other information
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email,user.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }
            //create token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                           configuration["Jwt:Audience"],
                                           claims,
                                           expires: DateTime.Now.AddMinutes(15),
                                           signingCredentials:credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       
    }
}

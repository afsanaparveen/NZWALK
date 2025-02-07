﻿

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.models.DTO;
using NZWalks.API.repositories;

namespace NZWalks.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase

    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }



        [HttpPost]
        [Route("Register")]
       public async Task<IActionResult>Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            //we need to register uder who gives the username and password
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityResult=await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if(identityResult.Succeeded)
            {
                //add roles to user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult=await userManager.AddToRolesAsync(identityUser,
                                                    registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        
                        return Ok("User was registered please login");
                    }

                }

            }
            return BadRequest("something went wrong");

        }
        //the login method
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult>Login([FromBody]LoginRequestDto loginRequestDto)
        {
           var user= await userManager.FindByEmailAsync(loginRequestDto.Username);
            if(user != null)
            {
               var checkPasswordResult= await userManager.CheckPasswordAsync(user,loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        var jwtToken=tokenRepository.CreateJWTToken(user,roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    } 
                }
            }
            return BadRequest("user was not found");
        }
    }
   
}

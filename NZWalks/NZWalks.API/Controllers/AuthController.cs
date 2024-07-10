using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenRepository _tokenRepository;

    public AuthController(
        UserManager<IdentityUser> userManager,
        ITokenRepository tokenRepository)
    {
        _userManager = userManager;
        _tokenRepository = tokenRepository;
    }

    // Register
    // POST: /api/auth/register
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        var identityUser = new IdentityUser
        {
            UserName = registerRequestDto.Username,
            Email = registerRequestDto.Username
        };

        var identityUserResult =
            await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

        if (identityUserResult.Succeeded)
        {
            // Add roles to the user.
            if (registerRequestDto.Roles is not null && registerRequestDto.Roles.Any())
            {
                identityUserResult =
                    await _userManager
                        .AddToRolesAsync(identityUser, registerRequestDto.Roles);

                if (identityUserResult.Succeeded)
                {
                    return Ok("User is registered! Please Login.");
                }
            }
        }

        return BadRequest("Something went wrong...!");
    }

    // POST: api/auth/login
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);

        if (user is not null)
        {
            var checkPasswordResult =
                await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (checkPasswordResult)
            {
                // Get Roles for this user.
                var roles = await _userManager.GetRolesAsync(user);

                if (roles is not null)
                {
                    // Create Token
                    var jwtToken = _tokenRepository.CreateJwtToken(user, roles.ToList());

                    var response = new LoginResponseDto
                    {
                        JwtToken = jwtToken,
                    };

                    return Ok(response);
                }
            }
        }
    
        return BadRequest("Username or password incorrect");
    }

}

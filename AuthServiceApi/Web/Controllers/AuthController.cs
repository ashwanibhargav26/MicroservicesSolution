using AuthServiceApi.Application.Common.Interfaces;
using AuthServiceApi.Application.Common.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServiceApi.Web.Controller;

public class AuthController(IAuthService userWriteService) : BaseController
{
    private readonly IAuthService _userService = userWriteService;

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(UserSignInRequest request)    {
        var result = await _userService.SignIn(request);
        if (result == null)
        {
            return Unauthorized(new { Message = "Invalid credentials. Please try again." });
        }
        return Ok(new { Message = "Sign-in successful.", Data = result });
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(UserSignUpRequest request, CancellationToken token)
    {
        var result = await _userService.SignUp(request, token);
        if (result == null)
        {
            return BadRequest(new { Message = "Sign-up failed. Please check your input and try again." });
        }
        return CreatedAtAction(nameof(GetProfile), new { id = result.Id }, new { Message = "Sign-up successful.", Data = result });
    }

    [HttpDelete("logout")]
    public IActionResult Logout()
    {
        _userService.Logout();
        return Ok(new { Message = "Logout successful." });
    }

    [HttpGet("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        var result = await _userService.RefreshToken();
        if (result == null)
        {
            return Unauthorized(new { Message = "Token refresh failed. Please sign in again." });
        }
        return Ok(new { Message = "Token refreshed successfully.", Data = result });
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var profile = await _userService.GetProfile();
        if (profile == null)
        {
            return NotFound(new { Message = "User profile not found." });
        }
        return Ok(new { Message = "Profile retrieved successfully.", Data = profile });
    }

}

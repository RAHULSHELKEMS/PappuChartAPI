using Microsoft.AspNetCore.Mvc;
using PappuPictureChart.API.Data;
using PappuPictureChart.API.Models;
using PappuPictureChart.API.Services;

namespace PappuPictureChart.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;
    private readonly JwtService _jwt;

    public AuthController(
        AuthService auth,
        JwtService jwt)
    {
        _auth = auth;
        _jwt = jwt;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterRequest request)
    {
        var user = await _auth.Register(
            request.Name,
            request.Mobile,
            request.Password);

        if (user == null)
            return BadRequest("Mobile Exists");

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequest request)
    {
        var user = await _auth.Login(
            request.Mobile,
            request.Password);

        if (user == null)
            return Unauthorized();

        var token = _jwt.GenerateToken(user);

        return Ok(new
        {
            token,
            user.Id,
            user.Name,
            user.Coins
        });
    }
}

public class RegisterRequest
{
    public string Name { get; set; } = "";
    public string Mobile { get; set; } = "";
    public string Password { get; set; } = "";
}

public class LoginRequest
{
    public string Mobile { get; set; } = "";
    public string Password { get; set; } = "";
}
using Microsoft.AspNetCore.Mvc;
using SecureGate.Interfaces;
using SecureGate.Services;

namespace SecureGate.Controllers;

[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
        _jwtTokenGenerator = new JwtTokenGenerator();
    }
    
    [HttpPost("Login", Name = "Login")]
    public IActionResult Login([FromBody] ILoginPayload responseBody)
    {
        if (string.IsNullOrEmpty(responseBody.ClientId) || string.IsNullOrEmpty(responseBody.ClientSecret))
        {
            return Ok();
        }

        JwtPayload payload = new JwtPayload("xxx", 3600);
        
        Console.WriteLine(payload.ToJsonString());

        string token = _jwtTokenGenerator.GenerateToken(payload, _configuration["JwtSettings:Secret"]!);
        
        var response = new 
        {
            Message = "Login successful",
            Status = "Success",
            Token = token
        };

        return Ok(response);
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System;
using TravelAPI.Models;
using Microsoft.Extensions.Configuration;

namespace TravelAPI.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class SecurityController : ControllerBase
  {
    private readonly IConfiguration _config;
    public SecurityController(IConfiguration configuration)
    {
      _config = configuration;
    }
    private string GenerateJWT()
    {
      var issuer = _config["Jwt:Issuer"];
      Console.WriteLine(issuer);
      var audience = _config["Jwt:Audience"];
      var expiry = DateTime.Now.AddMinutes(120);
      var securityKey = new SymmetricSecurityKey
  (Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
      var credentials = new SigningCredentials
  (securityKey, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(issuer: issuer,
  audience: audience,
  expires: DateTime.Now.AddMinutes(120),
  signingCredentials: credentials);

      var tokenHandler = new JwtSecurityTokenHandler();
      var stringToken = tokenHandler.WriteToken(token);
      return stringToken;
    }

    private bool ValidateUser(User loginDetails)
    {
      if (loginDetails.UserName == "User1" &&
        loginDetails.Password == "pass$word")
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    [HttpPost]
    [Route("api/security/login")]
    public IActionResult Login([FromBody] User loginDetails)
    {
      bool result = ValidateUser(loginDetails);
      if (result)
      {
        var tokenString = GenerateJWT();
        return Ok(new { token = tokenString });
      }
      else
      {
        return Unauthorized();
      }
    }
  }
}
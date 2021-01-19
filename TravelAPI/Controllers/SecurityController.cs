using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using TravelAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TravelAPI.ViewModels;

namespace TravelAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SecurityController : ControllerBase
  {
    private readonly IConfiguration _config;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly TravelAPIContext _db;
    public SecurityController(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TravelAPIContext db)
    {
      _config = configuration;
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }
    private string GenerateJWT(string user)
    {
      var issuer = _config["Jwt:Issuer"];
      var audience = user;
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

    [HttpPost]
    [Route("/api/security/login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel loginDetails)
    {
      // Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginDetails.Name, loginDetails.Password, isPersistent: true, lockoutOnFailure: false);
      var user = await _userManager.FindByNameAsync(loginDetails.Name);
      var result = await _signInManager.UserManager.CheckPasswordAsync(user, loginDetails.Password);

      if (result)
      {
        var tokenString = GenerateJWT(loginDetails.Name);
        return Ok(new { token = tokenString });
      }
      else
      {
        return Unauthorized();
      }
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet]
    public IActionResult Get()
    {
      List<Employee> data = new List<Employee>();
      data.Add(new Employee()
      {
        EmployeeID = 1,
        FirstName = "Nancy",
        LastName = "Davolio"
      });
      data.Add(new Employee()
      {
        EmployeeID = 2,
        FirstName = "Andrew",
        LastName = "Smith"
      });
      data.Add(new Employee()
      {
        EmployeeID = 3,
        FirstName = "Janet",
        LastName = "Rollings"
      });
      return new ObjectResult(data);
    }
    public class Employee
    {
      public int EmployeeID { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
    }

    [HttpPost]
    [Route("/api/security/register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
      ApplicationUser user = new ApplicationUser { UserName = model.Name };
      IdentityResult result = await _userManager.CreateAsync(user, model.Password);
      if (result.Succeeded)
      {
        _db.SaveChanges();
        return new OkObjectResult("Account created");
      }
      else
      {
        return new BadRequestObjectResult("Something went wrong account not created");
      }
    }
  }
}



using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using ReactTravelAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ReactTravelAPI.ViewModels;

namespace ReactTravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ReactTravelAPIContext _db;
        public SecurityController(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ReactTravelAPIContext db)
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
            var expiry = DateTime.Now.AddMinutes(240);
            var securityKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials
        (securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: issuer,
        audience: audience,
        expires: DateTime.Now.AddMinutes(240),
        signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

        [HttpPost]
        [Route("/api/security/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginDetails)
        {
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



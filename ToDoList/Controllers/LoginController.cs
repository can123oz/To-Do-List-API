using Entity.Context;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private DatabaseContext _db;

        public LoginController(IConfiguration config, DatabaseContext db)
        {
            _config = config;
            _db = db;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            var userLogged = Authenticate(user);
            if (userLogged != null)
            {
                var token = Generate(userLogged);
                return Ok(token);
            }
            return NotFound("User Not Found.");

        }

        private string Generate(User userLogged)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userLogged.UserName),
                new Claim(ClaimTypes.Email, userLogged.Email),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(User user)
        {
            var loggedUser = _db.Users.FirstOrDefault(p => p.Email == user.Email && p.Password == user.Password);
            if (loggedUser != null)
            {
                return loggedUser;
            }
            return null;
        }
    }
}

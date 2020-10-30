using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProyectUMG.Common;
using ProyectUMG.Models;

namespace ProyectUMG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ProyectContext _context;
        //private List<Models.User> appUsers = new List<User>
        //  {
        // new User {UserId=55, Name="test",Email="test@test.com",Password="123456",UserRole="Admin"}
        //new User { FullName = “Vaibhav Bhapkar”, UserName = “admin”, Password = “1234”, UserRole = “Admin” },
        //new User { FullName = “Test User”, UserName = “user”, Password = “1234”, UserRole = “User” }
        //};
        public LoginController(IConfiguration config,ProyectContext context)
        {
           _config = config;
            _context = context;
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Login login )
        {
            User user = AuthenticateUser(login.Email,login.Password);
            
            IActionResult response = Unauthorized();

            //response = Ok(Email);
            if (user != null)
            {
               // response = Ok("entro");
                var tokenString = GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }

            return response;

        }

        User AuthenticateUser(string Email, string Password)
        {
           var pass = CommunMethods.ConvertToEncrypt(Password);
            //return await _context.UserInfo.FirstOrDefaultAsync(u => u.Email == email && u.Password == password)
            User user = _context.User.FirstOrDefault(u => u.Email == Email && u.Password == pass);
           //User user = User.SingleOrDefault(x => x.Email == loginCredentials.Email && x.Password == loginCredentials.Password);
            return user;
        }

        string GenerateJWTToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Name),
                new Claim("email", userInfo.Email.ToString()),
                new Claim("role",userInfo.UserRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}

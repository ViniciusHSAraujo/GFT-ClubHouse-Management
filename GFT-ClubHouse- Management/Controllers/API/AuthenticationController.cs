using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using GFT_ClubHouse__Management.Libs.Login;
using GFT_ClubHouse__Management.Libs.Security;
using GFT_ClubHouse__Management.Libs.Utils;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.Enum;
using GFT_ClubHouse__Management.Models.ViewModels.API.UserViewModels;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GFT_ClubHouse__Management.Controllers.API {
    
    [Route("api/")]
    public class AuthenticationController : Controller {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private static readonly MD5HashTools MD5HashTools = new MD5HashTools();

        public AuthenticationController(IUserRepository userRepository,
            IConfiguration configuration) {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [Route("v1/login/")]
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginViewModel user) {
            var authenticatedUser =
                _userRepository.Login(user.Email, MD5HashTools.ReturnMD5(user.Password), UserRoles.Admin);

            if (authenticatedUser == null) {
                return ResponseUtils.GenerateObjectResult("Login failed");
            }
            
            var tokenString = JwtGenerator();

            return ResponseUtils.GenerateObjectResult("Authorized", new {token = tokenString});

        }

        private string JwtGenerator() {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer, audience: audience,
                expires: expiry, signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

    }
}
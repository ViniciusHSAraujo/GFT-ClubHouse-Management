using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Text;
using GFT_ClubHouse__Management.Libs.Security;
using GFT_ClubHouse__Management.Libs.Utils;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.Enum;
using GFT_ClubHouse__Management.Models.ViewModels.API;
using GFT_ClubHouse__Management.Models.ViewModels.API.UserViewModels;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GFT_ClubHouse__Management.Controllers.API {
    [Route("api/")]
    public class AuthenticationController : Controller {
        private static readonly MD5HashTools MD5HashTools = new MD5HashTools();
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IUserRepository userRepository,
            IConfiguration configuration) {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Login to get the Authorization KEY.
        /// </summary>
        /// <response code="200">Authorized. Returns a token valid for 120 minutes to be sent in requests.</response>
        /// <response code="401">Not Authorized.</response>
        [Route("v1/login/")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginViewModel user) {
            var authenticatedUser =
                _userRepository.Login(user.Email, MD5HashTools.ReturnMD5(user.Password), UserRoles.Admin);

            if (authenticatedUser == null) {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return ResponseUtils.GenerateObjectResult("Username or Password is invalid!");
            }

            var tokenString = JwtGenerator();
            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Authorized", tokenString);
        }

        private string JwtGenerator() {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer, audience,
                expires: expiry, signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
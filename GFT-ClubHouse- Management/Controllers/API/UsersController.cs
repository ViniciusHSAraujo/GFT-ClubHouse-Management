using System;
using System.Linq;
using GFT_ClubHouse__Management.Libs.ExtensionsMethods;
using GFT_ClubHouse__Management.Libs.Utils;
using Microsoft.AspNetCore.Mvc;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.ViewModels;
using GFT_ClubHouse__Management.Models.ViewModels.API.ClubHouseViewModels;
using GFT_ClubHouse__Management.Models.ViewModels.API.MusicalGenreViewModels;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GFT_ClubHouse__Management.Controllers.API {
    [Route("api/")]
    public class UsersController : Controller {
        private readonly IUserRepository _userRepository;
        
        public UsersController(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("v1/users/")]
        public ObjectResult Get() {
            var users = _userRepository.GetAllSimplified();

            if (!users.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Users not found!");
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Users successfully found!", users);
        }

        [HttpGet]
        [Route("v1/users/{id}")]
        public ObjectResult GetAsc(int id) {
            var user = _userRepository.GetSimplified(id);

            if (user == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("User not found!");
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("User successfully found!", user);
        }
    }
}
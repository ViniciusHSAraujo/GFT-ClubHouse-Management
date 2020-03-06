using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using GFT_ClubHouse__Management.Libs.Utils;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.ViewModels.API;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Controllers.API {
    [Route("api/")]
    [Authorize]
    public class UsersController : Controller {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        /// <summary>
        /// List Users.
        /// </summary>
        /// <response code="200">Returns a list of Users.</response>
        /// <response code="401">Not authorized! Log in first and send the validation token in the request.</response>
        /// <response code="404">There is no Users registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<IEnumerable<User>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Search for a User with the specified ID.
        /// </summary>
        /// <response code="200">Returns a User with the specified ID.</response>
        /// <response code="401">Not authorized! Log in first and send the validation token in the request.</response>
        /// <response code="404">There is no User registered with this ID.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
        [Route("v1/users/{id}")]
        public ObjectResult GetById(int id) {
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
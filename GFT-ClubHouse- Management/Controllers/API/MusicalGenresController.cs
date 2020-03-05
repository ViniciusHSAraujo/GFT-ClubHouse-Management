using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using GFT_ClubHouse__Management.Libs.ExtensionsMethods;
using GFT_ClubHouse__Management.Libs.Utils;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.ViewModels.API;
using GFT_ClubHouse__Management.Models.ViewModels.API.MusicalGenreViewModels;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Controllers.API {
    [Route("api/")]
    [Authorize]
    public class MusicalGenresController : Controller {
        private readonly IMusicalGenreRepository _musicalGenreRepository;

        public MusicalGenresController(IMusicalGenreRepository musicalGenreRepository) {
            _musicalGenreRepository = musicalGenreRepository;
        }

        /// <summary>
        /// List Musical Genres.
        /// </summary>
        /// <response code="200">Returns a list of Musical Genres.</response>
        /// <response code="404">There is no Musical Genres registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<IEnumerable<MusicalGenre>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
        [Route("v1/musicalgenres/")]
        public ObjectResult Get() {
            var musicalGenres = _musicalGenreRepository.GetAll();

            if (!musicalGenres.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Musical Genres not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Musical Genres successfully found!", musicalGenres);
        }

        /// <summary>
        /// List Musical Genres ordered by name ascending.
        /// </summary>
        /// <response code="200">Returns a list of Musical Genres ordered by name ascending.</response>
        /// <response code="404">There is no Musical Genres registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<IEnumerable<MusicalGenre>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("v1/musicalgenres/asc")]
        public ObjectResult GetAsc() {
            var musicalGenres = _musicalGenreRepository.GetAll().OrderBy(x => x.Name);

            if (!musicalGenres.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Musical genres not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Musical genres successfully found!", musicalGenres);
        }

        /// <summary>
        /// List Musical Genres ordered by name descending.
        /// </summary>
        /// <response code="200">Returns a list of Musical Genres ordered by name descending.</response>
        /// <response code="404">There is no Musical Genres registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<IEnumerable<MusicalGenre>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("v1/musicalgenres/desc")]
        public ObjectResult GetDesc() {
            var musicalGenres = _musicalGenreRepository.GetAll().OrderByDescending(x => x.Name);

            if (!musicalGenres.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Musical genre not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Musical genre successfully found!", musicalGenres);
        }

        /// <summary>
        /// Search for a Musical Genre with the specified ID.
        /// </summary>
        /// <response code="200">Returns a Musical Genre with the specified ID.</response>
        /// <response code="404">There is no Musical Genre registered with this ID.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<MusicalGenre>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("v1/musicalgenres/{id}")]
        public ObjectResult GetById(int id) {
            var musicalGenre = _musicalGenreRepository.GetById(id);

            if (musicalGenre == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Musical genre not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Musical genre successfully found!", musicalGenre);
        }

        /// <summary>
        /// Search Event by name.
        /// </summary>
        /// <param name="name">Name for search</param>
        /// <response code="200">Returns a list of Events that was found.</response>
        /// <response code="404">In this search nothing was found.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<IEnumerable<Event>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("v1/musicalgenres/name/{name}")]
        public ObjectResult GetByName(string name) {
            var musicalGenres = _musicalGenreRepository.GetAllByName(name);

            if (musicalGenres == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Musical genres not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Musical genres successfully found by name!", musicalGenres);
        }

        /// <summary>
        /// Create a new Musical Genre.
        /// </summary>
        /// <response code="201">Returns the created Musical Genre.</response>
        /// <response code="400">Returns a list of strings describing validation errors.</response>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Event>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>), StatusCodes.Status400BadRequest)]
        [Route("v1/musicalgenres/")]
        public ObjectResult Post([FromBody] MusicalGenreCreateViewModel musicalGenreTemp) {
            if (musicalGenreTemp == null) {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Error when registering the musical genre!", "Invalid model received.");
            }
            
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Error when registering the musical genre.",
                    ModelState.ListErrors());
            }

            var musicalGenre = new MusicalGenre() {
                Id = 0,
                Name = musicalGenreTemp.Name,
                IsActive = true
            };

            Response.StatusCode = StatusCodes.Status200OK;
            _musicalGenreRepository.Insert(musicalGenre);
            return ResponseUtils.GenerateObjectResult("Musical genre successfully registered!", musicalGenre);
        }

        /// <summary>
        /// Edit a Musical Genre.
        /// </summary>
        /// <response code="200">Returns the Musical Genre that was edited</response>
        /// <response code="400">Returns a list of strings describing validation errors.</response>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<MusicalGenre>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>), StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Route("v1/musicalgenres/{id}")]
        public ObjectResult Put(int id, [FromBody] MusicalGenreEditViewModel musicalGenreTemp) {
            if (musicalGenreTemp == null) {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Error when editing the musical genre!", "Invalid model received.");
            }
            
            if (id != musicalGenreTemp.Id)
                ModelState.AddModelError("Id", "Request Id differs from musical genre Id passed in body");

            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Error when editing the Musical Genre",
                    ModelState.ListErrors());
            }

            var musicalGenre = new MusicalGenre() {
                Id = musicalGenreTemp.Id,
                Name = musicalGenreTemp.Name,
                IsActive = musicalGenreTemp.IsActive
            };

            Response.StatusCode = StatusCodes.Status200OK;
            _musicalGenreRepository.Update(musicalGenre);
            return ResponseUtils.GenerateObjectResult("Musical Genre successfully edited!", musicalGenre);
        }

        /// <summary>
        /// Delete a Musical Genre.
        /// </summary>
        /// <response code="200">Returns the deleted Musical Genre.</response>
        /// <response code="404">Doesn't exist any Musical Genre with the ID.</response>
        /// <response code="406">This Musical Genre can't be deleted because there are restricted relationships.</response>
        [HttpDelete]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<MusicalGenre>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultViewModel<MusicalGenre>), StatusCodes.Status406NotAcceptable)]
        [HttpDelete]
        [Route("v1/musicalgenres/{id}")]
        public ObjectResult Delete(int id) {
            var musicalGenre = _musicalGenreRepository.GetById(id);

            if (musicalGenre == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Non-existent Musical Genre.", null);
            }

            try {
                Response.StatusCode = StatusCodes.Status200OK;
                _musicalGenreRepository.Delete(musicalGenre.Id);
                return ResponseUtils.GenerateObjectResult("Musical Genre successfully deleted!", musicalGenre);
            }
            catch (Exception) {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Unable to delete the Musical Genre, contact support!",
                    musicalGenre);
            }
        }
    }
}
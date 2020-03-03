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
    public class MusicalGenresController : Controller {
        private readonly IMusicalGenreRepository _musicalGenreRepository;

        public MusicalGenresController(IMusicalGenreRepository musicalGenreRepository) {
            _musicalGenreRepository = musicalGenreRepository;
        }

        [HttpGet]
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

        [HttpGet]
        [Route("v1/musicalgenres/{id}")]
        public ObjectResult Get(int id) {
            var musicalGenre = _musicalGenreRepository.GetById(id);

            if (musicalGenre == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Musical genre not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Musical genre successfully found!", musicalGenre);
        }

        [HttpGet]
        [Route("v1/musicalgenres/name/{name}")]
        public ObjectResult Get(string name) {
            var musicalGenres = _musicalGenreRepository.GetAllByName(name);

            if (musicalGenres == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Musical genres not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Musical genres successfully found by name!", musicalGenres);
        }

        [HttpPost]
        [Route("v1/musicalgenres/")]
        public ObjectResult Post([FromBody] MusicalGenreCreateViewModel musicalGenreTemp) {
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Error when registering the musical genre.",
                    ModelState.ListErrors());
            }

            MusicalGenre musicalGenre = new MusicalGenre() {
                Id = 0,
                Name = musicalGenreTemp.Name,
                IsActive = true
            };

            Response.StatusCode = StatusCodes.Status200OK;
            _musicalGenreRepository.Insert(musicalGenre);
            return ResponseUtils.GenerateObjectResult("Musical genre successfully registered!", musicalGenre);
        }


        [HttpPut]
        [Route("v1/musicalgenres/{id}")]
        public ObjectResult Put(int id, [FromBody] MusicalGenreEditViewModel musicalGenreTemp) {
            if (id != musicalGenreTemp.Id) {
                ModelState.AddModelError("Id", "Request Id differs from musical genre Id passed in body");
            }

            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Error when editing the Musical Genre",
                    ModelState.ListErrors());
            }

            MusicalGenre musicalGenre = new MusicalGenre() {
                Id = musicalGenreTemp.Id,
                Name = musicalGenreTemp.Name,
                IsActive = musicalGenreTemp.IsActive
            };

            Response.StatusCode = StatusCodes.Status200OK;
            _musicalGenreRepository.Update(musicalGenre);
            return ResponseUtils.GenerateObjectResult("Musical Genre successfully edited!", musicalGenre);
        }

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
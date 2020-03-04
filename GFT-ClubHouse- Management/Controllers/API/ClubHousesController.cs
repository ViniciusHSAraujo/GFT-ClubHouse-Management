using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using GFT_ClubHouse__Management.Libs.ExtensionsMethods;
using GFT_ClubHouse__Management.Libs.Utils;
using Microsoft.AspNetCore.Mvc;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.ViewModels;
using GFT_ClubHouse__Management.Models.ViewModels.API;
using GFT_ClubHouse__Management.Models.ViewModels.API.ClubHouseViewModels;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GFT_ClubHouse__Management.Controllers.API {
    [Route("api/")]
    public class ClubHousesController : Controller {
        private readonly IClubHouseRepository _clubHouseRepository;
        private readonly IAddressRepository _addressRepository;

        public ClubHousesController(IClubHouseRepository clubHouseRepository, IAddressRepository addressRepository) {
            _clubHouseRepository = clubHouseRepository;
            _addressRepository = addressRepository;
        }

        /// <summary>
        /// List Club Houses.
        /// </summary>
        /// <response code="200">Returns a list of Club Houses.</response>
        /// <response code="404">There is no Club House registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<IEnumerable<ClubHouse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
        [Route("v1/clubhouses/")]
        public ObjectResult Get() {
            var clubHouses = _clubHouseRepository.GetAll();

            if (!clubHouses.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Club House not found!");
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Club Houses successfully found!", clubHouses);
        }
        
         /// <summary>
        /// List Club Houses ordered by name ascending.
        /// </summary>
        /// <response code="200">Returns a list of Club Houses ordered by name ascending.</response>
        /// <response code="404">There is no Club House registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<IEnumerable<ClubHouse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
        [Route("v1/clubhouses/asc")]
        public ObjectResult GetAsc() {
            var clubHouses = _clubHouseRepository.GetAll().OrderBy(x => x.Name);

            if (!clubHouses.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Club House not found!");
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Club Houses successfully found!", clubHouses);
        }

        /// <summary>
        /// List Club Houses ordered by name descending.
        /// </summary>
        /// <response code="200">Returns a list of Club Houses ordered by name descending.</response>
        /// <response code="404">There is no Club House registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<IEnumerable<ClubHouse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
        [Route("v1/clubhouses/desc")]
        public ObjectResult GetDesc() {
            var clubHouses = _clubHouseRepository.GetAll().OrderByDescending(x => x.Name);

            if (!clubHouses.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Club House not found!");
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Club Houses successfully found!", clubHouses);
        }

        
        /// <summary>
        /// Search for a Club Houses with the specified ID.
        /// </summary>
        /// <response code="200">Returns a Club House with the specified ID.</response>
        /// <response code="404">There is no Club House registered with this ID.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<ClubHouse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
        [Route("v1/clubhouses/{id}")]
        public ObjectResult GetById(int id) {
            var clubHouse = _clubHouseRepository.GetById(id);

            if (clubHouse == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Club House not found!");
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Club House successfully found!", clubHouse);
        }

        /// <summary>
        /// Search Club Houses by name.
        /// </summary>
        /// <param name="name">Name for search</param>
        /// <response code="200">Returns a list of Club Houses that was found.</response>
        /// <response code="404">In this search nothing was found.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<IEnumerable<ClubHouse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
        [Route("v1/clubhouses/name/{name}")]
        public ObjectResult GetByName(string name) {
            var clubHouses = _clubHouseRepository.GetAllByName(name);

            if (!clubHouses.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Club House not found!");
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Club Houses successfully found by name!", clubHouses);
        }
        
       
        /// <summary>
        /// Create a new Club House.
        /// </summary>
        /// <response code="201">Returns the created Club House.</response>
        /// <response code="400">Returns a list of strings describing validation errors.</response>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<ClubHouse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>), StatusCodes.Status400BadRequest)]
        [Route("v1/clubhouses/")]
        public ObjectResult Post([FromBody] ClubHouseCreateViewModel clubHouseTemp) {
            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Error when registering the club house.",
                    ModelState.ListErrors());
            }

            var clubhouse = new ClubHouse() {
                Id = 0,
                Name = clubHouseTemp.Name,
                Address = new Address() {
                    Id = 0,
                    Street = clubHouseTemp.Street,
                    State = clubHouseTemp.State,
                    City = clubHouseTemp.City,
                    Zip = clubHouseTemp.Zip
                }
            };

            Response.StatusCode = StatusCodes.Status200OK;
            _clubHouseRepository.Insert(clubhouse);
            return ResponseUtils.GenerateObjectResult("Club House successfully registered!", clubhouse);
        }

        /// <summary>
        /// Edit a Club House.
        /// </summary>
        /// <response code="200">Returns the Club House that was edited</response>
        /// <response code="400">Returns a list of strings describing validation errors.</response>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<ClubHouse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>), StatusCodes.Status400BadRequest)]
        [Route("v1/clubhouses/{id}")]
        public ObjectResult Put(int id, [FromBody] ClubHouseEditViewModel clubHouseTemp) {
            if (id != clubHouseTemp.Id) {
                ModelState.AddModelError("Id", "Request Id differs from Club House Id passed in body");
            }

            if (!_clubHouseRepository.Exists(clubHouseTemp.AddressId)) {
                ModelState.AddModelError("Id", "This Club House ID does not exist.");
            }

            if (!_addressRepository.Exists(clubHouseTemp.AddressId)) {
                ModelState.AddModelError("AddressId", "This Address ID does not exist.");
            }

            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Error when editing the club house", ModelState.ListErrors());
            }

            var clubHouse = new ClubHouse() {
                Id = clubHouseTemp.Id,
                Name = clubHouseTemp.Name,
                Address = new Address() {
                    Id = clubHouseTemp.AddressId,
                    Street = clubHouseTemp.Street,
                    State = clubHouseTemp.State,
                    City = clubHouseTemp.City,
                    Zip = clubHouseTemp.Zip
                }
            };

            Response.StatusCode = StatusCodes.Status200OK;
            _clubHouseRepository.Update(clubHouse);
            return ResponseUtils.GenerateObjectResult("Club House successfully edited!", clubHouse);
        }

        /// <summary>
        /// Delete a Club House.
        /// </summary>
        /// <response code="200">Returns the deleted Club House.</response>
        /// <response code="404">Doesn't exist any Club House with the ID.</response>
        /// <response code="406">This Club House can't be deleted because there are restricted relationships.</response>
        [HttpDelete]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<ClubHouse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultViewModel<ClubHouse>), StatusCodes.Status406NotAcceptable)]
        [Route("v1/clubhouses/{id}")]
        public ObjectResult Delete(int id) {
            var clubhouse = _clubHouseRepository.GetById(id);

            if (clubhouse == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Non-existent Club House.");
            }

            try {
                Response.StatusCode = StatusCodes.Status200OK;
                _clubHouseRepository.Delete(clubhouse.Id);
                return ResponseUtils.GenerateObjectResult("Club House successfully deleted!", clubhouse);
            }
            catch (Exception) {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Unable to delete the club house, contact support!",
                    clubhouse);
            }
        }
    }
}
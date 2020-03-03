using System;
using System.Linq;
using GFT_ClubHouse__Management.Libs.ExtensionsMethods;
using GFT_ClubHouse__Management.Libs.Utils;
using Microsoft.AspNetCore.Mvc;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.ViewModels;
using GFT_ClubHouse__Management.Models.ViewModels.API.ClubHouseViewModels;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GFT_ClubHouse__Management.Controllers.API {
    [Route("api/")]
    public class ClubHousesController : Controller {
        private readonly IClubHouseRepository _clubHouseRepository;
        private readonly IAddressRepository _addressRepository;

        public ClubHousesController(IClubHouseRepository clubHouseRepository, IAddressRepository addressRepository) {
            _clubHouseRepository = clubHouseRepository;
            _addressRepository = addressRepository;
        }

        [HttpGet]
        [Route("v1/clubhouses/")]
        public ObjectResult Get() {
            var clubHouses = _clubHouseRepository.GetAll();

            if (!clubHouses.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Club House not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Club Houses successfully found!", clubHouses);
        }

        [HttpGet]
        [Route("v1/clubhouses/asc")]
        public ObjectResult GetAsc() {
            var clubHouses = _clubHouseRepository.GetAll().OrderBy(x => x.Name);

            if (!clubHouses.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Club House not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Club Houses successfully found!", clubHouses);
        }

        [HttpGet]
        [Route("v1/clubhouses/desc")]
        public ObjectResult GetDesc() {
            var clubHouses = _clubHouseRepository.GetAll().OrderByDescending(x => x.Name);

            if (!clubHouses.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Club House not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Club Houses successfully found!", clubHouses);
        }

        [HttpGet]
        [Route("v1/clubhouses/{id}")]
        public ObjectResult Get(int id) {
            var clubHouse = _clubHouseRepository.GetById(id);

            if (clubHouse == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Club House not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Club House successfully found!", clubHouse);
        }

        [HttpGet]
        [Route("v1/clubhouses/name/{name}")]
        public ObjectResult Get(string name) {
            var clubHouse = _clubHouseRepository.GetAllByName(name);

            if (clubHouse == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Club House not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Club Houses successfully found by name!", clubHouse);
        }

        [HttpPost]
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


        [HttpPut]
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

        [HttpDelete]
        [Route("v1/clubhouses/{id}")]
        public ObjectResult Delete(int id) {
            var clubhouse = _clubHouseRepository.GetById(id);

            if (clubhouse == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Non-existent Club House.", null);
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
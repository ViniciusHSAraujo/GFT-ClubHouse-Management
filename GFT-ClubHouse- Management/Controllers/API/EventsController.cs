using System;
using System.Linq;
using GFT_ClubHouse__Management.Libs.ExtensionsMethods;
using GFT_ClubHouse__Management.Libs.Utils;
using Microsoft.AspNetCore.Mvc;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.ViewModels;
using GFT_ClubHouse__Management.Models.ViewModels.API.EventViewModels;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GFT_ClubHouse__Management.Controllers.API {
    [Route("api/")]
    public class EventsController : Controller {
        private readonly IEventRepository _eventRepository;
        private readonly IClubHouseRepository _clubHouseRepository;
        private readonly IMusicalGenreRepository _musicalGenreRepository;

        public EventsController(IEventRepository eventRepository, IMusicalGenreRepository musicalGenreRepository,
            IClubHouseRepository clubHouseRepository) {
            _eventRepository = eventRepository;
            _musicalGenreRepository = musicalGenreRepository;
            _clubHouseRepository = clubHouseRepository;
        }

        [HttpGet]
        [Route("v1/events/{id}")]
        public ObjectResult Get(int id) {
            var schEvent = _eventRepository.GetById(id);

            if (schEvent == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Event not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Event successfully found!", schEvent);
        }

        [HttpGet]
        [Route("v1/events/")]
        public ObjectResult Get() {
            var schEvents = _eventRepository.GetAll();

            if (!schEvents.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Events not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Events successfully found!", schEvents);
        }

        [HttpPost]
        [Route("v1/events/")]
        public ObjectResult Post([FromBody] EventCreateViewModel eventTemp) {
            if (!_clubHouseRepository.Exists(eventTemp.ClubHouseId)) {
                ModelState.AddModelError("ClubHouseId", "This Club House ID does not exist.");
            }

            if (!_clubHouseRepository.Exists(eventTemp.MusicalGenreId)) {
                ModelState.AddModelError("MusicalGenreId", "This Musical Genre ID does not exist.");
            }

            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Error when registering the club house.",
                    ModelState.ListErrors());
            }

            Event schEvent = new Event() {
                Id = 0,
                Name = eventTemp.Name,
                Capacity = eventTemp.Capacity,
                Date = eventTemp.Date,
                Price = eventTemp.Price,
                ClubHouseId = eventTemp.ClubHouseId,
                MusicalGenreId = eventTemp.ClubHouseId
            };

            Response.StatusCode = StatusCodes.Status200OK;
            _eventRepository.Insert(schEvent);
            return ResponseUtils.GenerateObjectResult("Event successfully registered!", schEvent);
        }


        [HttpPut]
        [Route("v1/events/{id}")]
        public ObjectResult Put(int id, [FromBody] EventEditViewModel eventTemp) {
            if (id != eventTemp.Id) {
                ModelState.AddModelError("Id", "Request Id differs from Event Id passed in body");
            }

            if (!_clubHouseRepository.Exists(eventTemp.ClubHouseId)) {
                ModelState.AddModelError("ClubHouseId", "This Club House ID does not exist.");
            }

            if (!_clubHouseRepository.Exists(eventTemp.MusicalGenreId)) {
                ModelState.AddModelError("MusicalGenreId", "This Musical Genre ID does not exist.");
            }

            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Error when editing the club house.",
                    ModelState.ListErrors());
            }

            Event schEvent = new Event() {
                Id = eventTemp.Id,
                Name = eventTemp.Name,
                Capacity = eventTemp.Capacity,
                Date = eventTemp.Date,
                Price = eventTemp.Price,
                ClubHouseId = eventTemp.ClubHouseId,
                MusicalGenreId = eventTemp.ClubHouseId
            };

            Response.StatusCode = StatusCodes.Status200OK;
            _eventRepository.Update(schEvent);
            return ResponseUtils.GenerateObjectResult("Club House successfully edited!", schEvent);
        }

        [HttpDelete]
        [Route("v1/events/{id}")]
        public ObjectResult Delete(int id) {
            var schEvent = _eventRepository.GetById(id);

            if (schEvent == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Non-existent Event.", null);
            }

            try {
                Response.StatusCode = StatusCodes.Status200OK;
                _eventRepository.Delete(schEvent.Id);
                return ResponseUtils.GenerateObjectResult("Event successfully deleted!", schEvent);
            }
            catch (Exception e) {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Unable to delete the event, contact support!",
                    schEvent);
            }
        }
        
        [HttpGet]
        [Route("v1/events/capacity/asc")]
        public ObjectResult GetCapacityAsc() {
            var schEvents = _eventRepository.GetAll().OrderBy(x => x.Capacity);

            if (!schEvents.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Events not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Events successfully found!", schEvents);
        }
        
        [HttpGet]
        [Route("v1/events/capacity/desc")]
        public ObjectResult GetCapacityDesc() {
            var schEvents = _eventRepository.GetAll().OrderByDescending(x => x.Capacity);

            if (!schEvents.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Events not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Events successfully found!", schEvents);
        }
        
        [HttpGet]
        [Route("v1/events/capacity/Date/asc")]
        public ObjectResult GetDateAsc() {
            var schEvents = _eventRepository.GetAll().OrderBy(x => x.Date);

            if (!schEvents.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Events not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Events successfully found!", schEvents);
        }
        
        [HttpGet]
        [Route("v1/events/capacity/Date/desc")]
        public ObjectResult GetDateDesc() {
            var schEvents = _eventRepository.GetAll().OrderByDescending(x => x.Date);

            if (!schEvents.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Events not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Events successfully found!", schEvents);
        }
        
        [HttpGet]
        [Route("v1/events/capacity/name/asc")]
        public ObjectResult GetNameAsc() {
            var schEvents = _eventRepository.GetAll().OrderBy(x => x.Name);

            if (!schEvents.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Events not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Events successfully found!", schEvents);
        }
        
        [HttpGet]
        [Route("v1/events/capacity/name/desc")]
        public ObjectResult GetNameDesc() {
            var schEvents = _eventRepository.GetAll().OrderByDescending(x => x.Name);

            if (!schEvents.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Events not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Events successfully found!", schEvents);
        }
        
        [HttpGet]
        [Route("v1/events/capacity/price/asc")]
        public ObjectResult GetPriceAsc() {
            var schEvents = _eventRepository.GetAll().OrderBy(x => x.Price);

            if (!schEvents.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Events not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Events successfully found!", schEvents);
        }
        
        [HttpGet]
        [Route("v1/events/capacity/price/desc")]
        public ObjectResult GetPriceDesc() {
            var schEvents = _eventRepository.GetAll().OrderByDescending(x => x.Price);

            if (!schEvents.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Events not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Events successfully found!", schEvents);
        }
    }
}
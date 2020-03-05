using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using GFT_ClubHouse__Management.Libs.ExtensionsMethods;
using GFT_ClubHouse__Management.Libs.Utils;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.ViewModels.API;
using GFT_ClubHouse__Management.Models.ViewModels.API.EventViewModels;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Controllers.API {
    [Route("api/")]
    [Authorize]
    public class EventsController : Controller {
        private readonly IClubHouseRepository _clubHouseRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMusicalGenreRepository _musicalGenreRepository;

        public EventsController(IEventRepository eventRepository, IMusicalGenreRepository musicalGenreRepository,
            IClubHouseRepository clubHouseRepository) {
            _eventRepository = eventRepository;
            _musicalGenreRepository = musicalGenreRepository;
            _clubHouseRepository = clubHouseRepository;
        }

        /// <summary>
        /// List Events.
        /// </summary>
        /// <response code="200">Returns a list of Events.</response>
        /// <response code="404">There is no Event registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<IEnumerable<Event>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// List Events ordered by capacity ascending.
        /// </summary>
        /// <response code="200">Returns a list of events ordered by capacity ascending.</response>
        /// <response code="404">There is no events registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// List Events ordered by capacity descending.
        /// </summary>
        /// <response code="200">Returns a list of events ordered by capacity descending.</response>
        /// <response code="404">There is no events registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// List Events ordered by date ascending.
        /// </summary>
        /// <response code="200">Returns a list of events ordered by date ascending.</response>
        /// <response code="404">There is no events registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// List Events ordered by date descending.
        /// </summary>
        /// <response code="200">Returns a list of events ordered by date descending.</response>
        /// <response code="404">There is no events registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// List Events ordered by name ascending.
        /// </summary>
        /// <response code="200">Returns a list of events ordered by name ascending.</response>
        /// <response code="404">There is no events registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// List Events ordered by name descending.
        /// </summary>
        /// <response code="200">Returns a list of events ordered by name descending.</response>
        /// <response code="404">There is no events registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// List Events ordered by price ascending.
        /// </summary>
        /// <response code="200">Returns a list of events ordered by price ascending.</response>
        /// <response code="404">There is no events registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// List Events ordered by price descending.
        /// </summary>
        /// <response code="200">Returns a list of events ordered by price descending.</response>
        /// <response code="404">There is no events registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Search for a Event with the specified ID.
        /// </summary>
        /// <response code="200">Returns a Event with the specified ID.</response>
        /// <response code="404">There is no Event registered with this ID.</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("v1/events/{id}")]
        public ObjectResult GetById(int id) {
            var schEvent = _eventRepository.GetById(id);

            if (schEvent == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Event not found!", null);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Event successfully found!", schEvent);
        }

        /// <summary>
        /// Create a new Event.
        /// </summary>
        /// <response code="201">Returns the created Event.</response>
        /// <response code="400">Returns a list of strings describing validation errors.</response>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Event>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>), StatusCodes.Status400BadRequest)]
        [Route("v1/events/")]
        public ObjectResult Post([FromBody] EventCreateViewModel eventTemp) {
            if (!_clubHouseRepository.Exists(eventTemp.ClubHouseId))
                ModelState.AddModelError("ClubHouseId", "This Club House ID does not exist.");

            if (!_clubHouseRepository.Exists(eventTemp.MusicalGenreId))
                ModelState.AddModelError("MusicalGenreId", "This Musical Genre ID does not exist.");

            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Error when registering the club house.",
                    ModelState.ListErrors());
            }

            var schEvent = new Event() {
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

        /// <summary>
        /// Edit a Event.
        /// </summary>
        /// <response code="200">Returns the Event that was edited</response>
        /// <response code="400">Returns a list of strings describing validation errors.</response>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>), StatusCodes.Status400BadRequest)]
        [Route("v1/events/{id}")]
        public ObjectResult Put(int id, [FromBody] EventEditViewModel eventTemp) {
            if (id != eventTemp.Id) ModelState.AddModelError("Id", "Request Id differs from Event Id passed in body");

            if (!_clubHouseRepository.Exists(eventTemp.ClubHouseId))
                ModelState.AddModelError("ClubHouseId", "This Club House ID does not exist.");

            if (!_clubHouseRepository.Exists(eventTemp.MusicalGenreId))
                ModelState.AddModelError("MusicalGenreId", "This Musical Genre ID does not exist.");

            if (!ModelState.IsValid) {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return ResponseUtils.GenerateObjectResult("Error when editing the club house.",
                    ModelState.ListErrors());
            }

            var schEvent = new Event() {
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

        /// <summary>
        /// Delete a Event.
        /// </summary>
        /// <response code="200">Returns the deleted Event.</response>
        /// <response code="404">Doesn't exist any Event with the ID.</response>
        /// <response code="406">This Event can't be deleted because there are restricted relationships.</response>
        [HttpDelete]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<List<string>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResultViewModel<Event>), StatusCodes.Status406NotAcceptable)]
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
            catch (Exception) {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return ResponseUtils.GenerateObjectResult("Unable to delete the event, contact support!",
                    schEvent);
            }
        }
    }
}
using System;
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
using GFT_ClubHouse__Management.Models.ViewModels.API.MusicalGenreViewModels;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GFT_ClubHouse__Management.Controllers.API {
    [Route("api/")]
    public class SalesController : Controller {
        private readonly ISaleRepository _saleRepository;
        
        public SalesController(ISaleRepository saleRepository) {
            _saleRepository = saleRepository;
        }
        
        /// <summary>
        /// List Sales.
        /// </summary>
        /// <response code="200">Returns a list of Sales.</response>
        /// <response code="404">There is no Sales registered.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<IEnumerable<ClubHouse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
        [Route("v1/sales/")]
        public ObjectResult Get() {
            var sales = _saleRepository.GetAll();

            if (!sales.Any()) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Sales not found!");
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("Sales successfully found!", sales);
        }

        
        /// <summary>
        /// Search for a sale with the specified ID.
        /// </summary>
        /// <response code="200">Returns a Sale with the specified ID.</response>
        /// <response code="404">There is no Sale registered with this ID.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResultViewModel<Sale>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<object>), StatusCodes.Status404NotFound)]
        [Route("v1/sales/{id}")]
        public ObjectResult GetById(int id) {
            var sale = _saleRepository.GetById(id);

            if (sale == null) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return ResponseUtils.GenerateObjectResult("Sale not found!");
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return ResponseUtils.GenerateObjectResult("User successfully found!", sale);
        }
    }
}
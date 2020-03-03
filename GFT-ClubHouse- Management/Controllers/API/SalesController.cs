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
    public class SalesController : Controller {
        private readonly ISaleRepository _saleRepository;
        
        public SalesController(ISaleRepository saleRepository) {
            _saleRepository = saleRepository;
        }

        [HttpGet]
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

        [HttpGet]
        [Route("v1/sales/{id}")]
        public ObjectResult GetAsc(int id) {
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
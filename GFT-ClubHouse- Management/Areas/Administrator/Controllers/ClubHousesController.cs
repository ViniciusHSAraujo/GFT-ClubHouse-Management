using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFT_ClubHouse__Management.Libs.Filters.Security;
using GFT_ClubHouse__Management.Libs.Language;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Areas.Administrator.Controllers {
    [Area("Administrator")]
    [AdminAuthorization]
    public class ClubHousesController : Controller {

        private readonly IClubHouseRepository _clubHouseRepository;

        public ClubHousesController(IClubHouseRepository clubHouseRepository) {
            _clubHouseRepository = clubHouseRepository;
        }

        public ActionResult Index(int? page, string search) {
            var clubHouses = _clubHouseRepository.GetAll(page, search);
            return View(clubHouses);
        }

        public ActionResult Details(int id) {
            var clubHouse = _clubHouseRepository.GetById(id);
            return View(clubHouse);
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] ClubHouse clubHouse) {
            if (ModelState.IsValid) {
                try {
                    _clubHouseRepository.Insert(clubHouse);
                    TempData["MSG_S"] = SuccessMessages.MSG_S001;
                    return RedirectToAction(nameof(Index));
                } catch (Exception) {
                    TempData["MSG_E"] = ErrorMessages.MSG_E007;
                }
            }
            return View();
        }

        public ActionResult Edit(int id) {
            var clubHouse = _clubHouseRepository.GetById(id);
            return View(clubHouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] ClubHouse clubHouse) {
            if (ModelState.IsValid) {
                try {
                    _clubHouseRepository.Update(clubHouse);
                    TempData["MSG_S"] = SuccessMessages.MSG_S002;
                    return RedirectToAction(nameof(Index));
                } catch (Exception) {
                    TempData["MSG_E"] = ErrorMessages.MSG_E007;
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id) {
            try {
                _clubHouseRepository.Delete(id);
                TempData["MSG_S"] = SuccessMessages.MSG_S003;
            } catch {
                TempData["MSG_E"] = ErrorMessages.MSG_E012;
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
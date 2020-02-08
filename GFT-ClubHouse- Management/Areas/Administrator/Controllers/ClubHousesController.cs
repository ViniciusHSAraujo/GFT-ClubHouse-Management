using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFT_ClubHouse__Management.Libs.Language;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Areas.Administrator.Controllers {
    [Area("Administrator")]
    public class ClubHousesController : Controller {

        private readonly IClubHouseRepository _clubHouseRepository;

        public ClubHousesController(IClubHouseRepository clubHouseRepository) {
            _clubHouseRepository = clubHouseRepository;
        }

        // GET: ClubHouses
        public ActionResult Index() {
            var clubHouses = _clubHouseRepository.GetAll();
            return View(clubHouses);
        }

        // GET: ClubHouses/Details/5
        public ActionResult Details(int id) {
            var clubHouse = _clubHouseRepository.GetById(id);
            return View(clubHouse);
        }

        // GET: ClubHouses/Create
        public ActionResult Create() {
            return View();
        }

        // POST: ClubHouses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] ClubHouse clubHouse) {
            if (ModelState.IsValid) {
                _clubHouseRepository.Insert(clubHouse);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: ClubHouses/Edit/5
        public ActionResult Edit(int id) {
            var clubHouse = _clubHouseRepository.GetById(id);
            return View(clubHouse);
        }

        // POST: ClubHouses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] ClubHouse clubHouse) {
            if (ModelState.IsValid) {
                _clubHouseRepository.Update(clubHouse);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // POST: ClubHouses/Delete/5
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
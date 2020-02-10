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
    public class EventsController : Controller {
        private readonly IEventRepository _eventRepository;
        private readonly IClubHouseRepository _clubHouseRepository;
        private readonly IMusicalGenreRepository _musicalGenreRepository;

        public EventsController(IEventRepository eventRepository, IClubHouseRepository clubHouseRepository,
            IMusicalGenreRepository musicalGenreRepository) {
            _eventRepository = eventRepository;
            _clubHouseRepository = clubHouseRepository;
            _musicalGenreRepository = musicalGenreRepository;
        }

        public ActionResult Index() {
            var events = _eventRepository.GetAll();
            return View(events);
        }

        public ActionResult Details(int id) {
            var event_ = _eventRepository.GetById(id);
            return View(event_);
        }

        public ActionResult Create() {
            ViewBag.ClubHouses = _clubHouseRepository.GetSelectList();
            ViewBag.MusicalGenres = _musicalGenreRepository.GetSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Event event_) {
            if (ModelState.IsValid) {
                try {
                    _eventRepository.Insert(event_);
                    TempData["MSG_S"] = SuccessMessages.MSG_S001;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e) {
                    TempData["MSG_E"] = e.Message;
                }
            }

            ViewBag.ClubHouses = _clubHouseRepository.GetSelectList();
            ViewBag.MusicalGenres = _musicalGenreRepository.GetSelectList();
            return View(event_);
        }

        public ActionResult Edit(int id) {
            var event_ = _eventRepository.GetById(id);
            ViewBag.ClubHouses = _clubHouseRepository.GetSelectList();
            ViewBag.MusicalGenres = _musicalGenreRepository.GetSelectList();
            ViewBag.SoldTickets = event_.Tickets.Count(x => x.IsSold);
            return View(event_);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] Event event_) {
            if (ModelState.IsValid) {
                try {
                    _eventRepository.Update(event_);
                    TempData["MSG_S"] = SuccessMessages.MSG_S002;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception) {
                    TempData["MSG_E"] = ErrorMessages.MSG_E007;
                }
            }

            ViewBag.ClubHouses = _clubHouseRepository.GetSelectList();
            ViewBag.MusicalGenres = _musicalGenreRepository.GetSelectList();
            ViewBag.SoldTickets = _eventRepository.GetById(event_.Id).Tickets.Count(x => x.IsSold);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id) {
            try {
                _eventRepository.Delete(id);
                TempData["MSG_S"] = SuccessMessages.MSG_S003;
            }
            catch {
                TempData["MSG_E"] = ErrorMessages.MSG_E012;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
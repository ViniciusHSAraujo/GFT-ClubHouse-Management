﻿using System;
using GFT_ClubHouse__Management.Libs.Filters.Security;
using GFT_ClubHouse__Management.Libs.Language;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Areas.Administrator.Controllers {
    [Area("Administrator")]
    [AdminAuthorization]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class EventsController : Controller {
        private readonly IClubHouseRepository _clubHouseRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMusicalGenreRepository _musicalGenreRepository;
        private readonly ITicketRepository _ticketRepository;

        public EventsController(IEventRepository eventRepository, IClubHouseRepository clubHouseRepository,
            IMusicalGenreRepository musicalGenreRepository, ITicketRepository ticketRepository) {
            _eventRepository = eventRepository;
            _clubHouseRepository = clubHouseRepository;
            _musicalGenreRepository = musicalGenreRepository;
            _ticketRepository = ticketRepository;
        }

        public ActionResult Index(int? page, string search) {
            var events = _eventRepository.GetAll(page, search);
            return View(events);
        }

        public ActionResult Details(int id) {
            var event_ = _eventRepository.GetById(id);
            if (event_ == null) return new NotFoundResult();
            return View(event_);
        }

        public ActionResult Create() {
            ViewBag.ClubHouses = _clubHouseRepository.GetSelectList();
            ViewBag.ClubHousesQuantity = _clubHouseRepository.Count();
            ViewBag.MusicalGenres = _musicalGenreRepository.GetSelectList();
            ViewBag.MusicalGenresQuantity = _clubHouseRepository.Count();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Event event_) {
            if (ModelState.IsValid)
                try {
                    _eventRepository.Insert(event_);
                    TempData["MSG_S"] = SuccessMessages.MSG_S001;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception) {
                    TempData["MSG_E"] = ErrorMessages.MSG_E007;
                }

            ViewBag.ClubHouses = _clubHouseRepository.GetSelectList();
            ViewBag.ClubHousesQuantity = _clubHouseRepository.Count();
            ViewBag.MusicalGenres = _musicalGenreRepository.GetSelectList();
            ViewBag.MusicalGenresQuantity = _clubHouseRepository.Count();
            return View(event_);
        }

        public ActionResult Edit(int id) {
            var event_ = _eventRepository.GetById(id);
            if (event_ == null) return new NotFoundResult();
            ViewBag.ClubHouses = _clubHouseRepository.GetSelectList();
            ViewBag.MusicalGenres = _musicalGenreRepository.GetSelectList();
            ViewBag.SoldTickets = _ticketRepository.CountTicketsSoldForAnEvent(id);
            return View(event_);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] Event event_) {
            if (ModelState.IsValid)
                try {
                    _eventRepository.Update(event_);
                    TempData["MSG_S"] = SuccessMessages.MSG_S002;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e) {
                    TempData["MSG_E"] = e.Message;
                }

            ViewBag.ClubHouses = _clubHouseRepository.GetSelectList();
            ViewBag.MusicalGenres = _musicalGenreRepository.GetSelectList();
            ViewBag.SoldTickets = _ticketRepository.CountTicketsSoldForAnEvent(event_.Id);
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
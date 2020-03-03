using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFT_ClubHouse__Management.Libs.Filters.Security;
using GFT_ClubHouse__Management.Libs.Language;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Areas.Administrator.Controllers {
    [Area("Administrator")]
    [AdminAuthorization]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MusicalGenresController : Controller {
        private readonly IMusicalGenreRepository _musicalGenreRepository;

        public MusicalGenresController(IMusicalGenreRepository musicalGenreRepository) {
            _musicalGenreRepository = musicalGenreRepository;
        }

        public ActionResult Index(int? page, string search) {
            var musicalGenres = _musicalGenreRepository.GetAll(page, search);
            return View(musicalGenres);
        }

        public ActionResult Details(int id) {
            var musicalGenre = _musicalGenreRepository.GetById(id);
            if (musicalGenre == null) {
                return new NotFoundResult();
            }
            return View(musicalGenre);
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] MusicalGenre musicalGenre) {
            if (ModelState.IsValid) {
                try {
                    _musicalGenreRepository.Insert(musicalGenre);
                    TempData["MSG_S"] = SuccessMessages.MSG_S001;
                    return RedirectToAction(nameof(Index));
                } catch (Exception) {
                    TempData["MSG_E"] = ErrorMessages.MSG_E007;
                }
            }
            return View();
        }

        public ActionResult Edit(int id) {
            var musicalGenre = _musicalGenreRepository.GetById(id);
            if (musicalGenre == null) {
                return new NotFoundResult();
            }
            return View(musicalGenre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] MusicalGenre musicalGenre) {
            if (ModelState.IsValid) {
                try {
                    _musicalGenreRepository.Update(musicalGenre);
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
                _musicalGenreRepository.Delete(id);
                TempData["MSG_S"] = SuccessMessages.MSG_S003;
            } catch {
                TempData["MSG_E"] = ErrorMessages.MSG_E012;
            }
            return RedirectToAction(nameof(Index));

        }
    }

}
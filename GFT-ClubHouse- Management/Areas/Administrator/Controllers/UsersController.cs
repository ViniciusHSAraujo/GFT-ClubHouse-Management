using System;
using System.Security;
using GFT_ClubHouse__Management.Libs.Filters.Security;
using GFT_ClubHouse__Management.Libs.Language;
using GFT_ClubHouse__Management.Libs.Security;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Areas.Administrator.Controllers {
    [Area("Administrator")]
    [AdminAuthorization]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UsersController : Controller {
        private static readonly MD5HashTools mD5HashTools = new MD5HashTools();
        private readonly IUserRepository _userRepository;


        public UsersController(IUserRepository userRepository) {
            _userRepository = userRepository;
        }


        public ActionResult Index(int? page, string search) {
            var users = _userRepository.GetAll(page, search);
            return View(users);
        }

        public ActionResult Details(int id) {
            var user = _userRepository.GetById(id);
            if (user == null) return new NotFoundResult();

            return View(user);
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] User user) {
            if (ModelState.IsValid)
                try {
                    user.Password = mD5HashTools.ReturnMD5(user.Password);
                    _userRepository.Insert(user);
                    TempData["MSG_S"] = SuccessMessages.MSG_S001;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e) {
                    TempData["MSG_E"] = e.Message;
                }

            return View(user);
        }

        public ActionResult Edit(int id) {
            var user = _userRepository.GetById(id);
            if (user == null) return new NotFoundResult();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] User user) {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
                try {
                    _userRepository.Update(user);
                    TempData["MSG_S"] = SuccessMessages.MSG_S002;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception) {
                    TempData["MSG_E"] = ErrorMessages.MSG_E007;
                }

            return View();
        }

        public ActionResult EditPassword(int id) {
            var user = _userRepository.GetById(id);
            if (user == null) return new NotFoundResult();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPassword([FromForm] User user) {
            if (ModelState["Password"].Errors.Count == 0 && ModelState["ConfirmPassword"].Errors.Count == 0)
                try {
                    var userDb = _userRepository.GetById(user.Id);
                    userDb.Password = mD5HashTools.ReturnMD5(user.Password);
                    _userRepository.Update(userDb);
                    TempData["MSG_S"] = SuccessMessages.MSG_S002;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception) {
                    TempData["MSG_E"] = ErrorMessages.MSG_E007;
                }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id) {
            try {
                _userRepository.Delete(id);
                TempData["MSG_S"] = SuccessMessages.MSG_S003;
            }
            catch (Exception e) {
                TempData["MSG_E"] = e.GetType() == typeof(SecurityException) ? e.Message : ErrorMessages.MSG_E012;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
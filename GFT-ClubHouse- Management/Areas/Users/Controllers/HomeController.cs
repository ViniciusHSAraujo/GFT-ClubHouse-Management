using System;
using GFT_ClubHouse__Management.Libs.Filters.Security;
using GFT_ClubHouse__Management.Libs.Language;
using GFT_ClubHouse__Management.Libs.Login;
using GFT_ClubHouse__Management.Libs.Security;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.Enum;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Areas.Users.Controllers {
    [Area("Users")]
    [Route("{area}/{action=Index}/{id?}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller {
        private static readonly MD5HashTools MD5HashTools = new MD5HashTools();
        private readonly LoginUser _loginUser;
        private readonly IUserRepository _userRepository;


        public HomeController(LoginUser loginUser, IUserRepository userRepository) {
            _loginUser = loginUser;
            _userRepository = userRepository;
        }

        public IActionResult Login() {
            return View();
        }

        [UserAuthorization]
        public IActionResult Account() {
            var userSession = _loginUser.GetUser();
            var userDb = _userRepository.GetById(userSession.Id);

            return View(userDb);
        }

        [UserAuthorization]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Account([FromForm] User user) {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
                try {
                    user.Roles = UserRoles.User;
                    _userRepository.Update(user);

                    _loginUser.Logout();
                    _loginUser.Login(user);
                    TempData["MSG_S"] = SuccessMessages.MSG_S002;
                    return RedirectToAction(nameof(Account));
                }
                catch (Exception) {
                    TempData["MSG_E"] = ErrorMessages.MSG_E007;
                }

            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] User user, string returnUrl = null) {
            ViewData["ReturnUrl"] = returnUrl;

            var authenticatedUser =
                _userRepository.Login(user.Email, MD5HashTools.ReturnMD5(user.Password), UserRoles.User);

            if (authenticatedUser != null) {
                _loginUser.Login(authenticatedUser);
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }

            TempData["MSG_E"] = ErrorMessages.MSG_E008;
            return View();
        }

        [UserAuthorization]
        public ActionResult EditPassword(int id) {
            var user = _userRepository.GetById(id);
            if (_loginUser.GetUser().Id == id)
                return View(user);
            else
                return new UnauthorizedResult();
        }

        [HttpPost]
        [UserAuthorization]
        [ValidateAntiForgeryToken]
        public ActionResult EditPassword([FromForm] User user) {
            if (ModelState["Password"].Errors.Count == 0 && ModelState["ConfirmPassword"].Errors.Count == 0)
                try {
                    var userDb = _userRepository.GetById(user.Id);
                    userDb.Password = MD5HashTools.ReturnMD5(user.Password);
                    _userRepository.Update(userDb);
                    TempData["MSG_S"] = SuccessMessages.MSG_S002;
                    return RedirectToAction("Index", "Orders", new {Area = "Users"});
                }
                catch (Exception) {
                    TempData["MSG_E"] = ErrorMessages.MSG_E007;
                }

            return View();
        }

        [UserAuthorization]
        public IActionResult Logout() {
            _loginUser.Logout();
            TempData["MSG_S"] = SuccessMessages.MSG_S007;
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        public IActionResult Register([FromForm] User user, string returnUrl = null) {
            if (ModelState.IsValid)
                try {
                    user.Password = MD5HashTools.ReturnMD5(user.Password);
                    user.Roles = UserRoles.User;
                    _userRepository.Insert(user);
                    _loginUser.Login(user);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e) {
                    TempData["MSG_E"] = e.Message;
                }

            return View();
        }
    }
}
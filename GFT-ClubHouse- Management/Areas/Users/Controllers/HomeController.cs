using System;
using GFT_ClubHouse__Management.Libs.Filters.Security;
using GFT_ClubHouse__Management.Libs.Language;
using GFT_ClubHouse__Management.Libs.Login;
using GFT_ClubHouse__Management.Libs.Security;
using GFT_ClubHouse__Management.Models.Enum;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Areas.Users.Controllers {
    [Area("Users")]
    [Route("{area}/{action=Index}")]
    public class HomeController : Controller {
        private readonly LoginUser _loginUser;
        private readonly IUserRepository _userRepository;
        private static readonly MD5HashTools MD5HashTools = new MD5HashTools();


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

        [HttpPost]
        public IActionResult Login([FromForm] Models.User user, string returnUrl = null) {
            ViewData["ReturnUrl"] = returnUrl;

            var authenticatedUser =
                _userRepository.Login(user.Email, MD5HashTools.ReturnMD5(user.Password), UserRoles.User);

            if (authenticatedUser != null) {
                _loginUser.Login(authenticatedUser);
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)){
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            TempData["MSG_E"] = ErrorMessages.MSG_E008;
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
        public IActionResult Register([FromForm] Models.User user, string returnUrl = null) {
            if (ModelState.IsValid) {
                try {
                    user.Password = MD5HashTools.ReturnMD5(user.Password);
                    user.Roles = UserRoles.User;
                    _userRepository.Insert(user);
                    _loginUser.Login(user);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)){
                        return Redirect(returnUrl);
                    }
                    
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e) {
                    TempData["MSG_E"] = e.Message;
                }
            }

            return View();
        }
    }
}
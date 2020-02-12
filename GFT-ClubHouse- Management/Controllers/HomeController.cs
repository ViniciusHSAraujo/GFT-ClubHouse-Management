using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GFT_ClubHouse__Management.Libs.Filters.Security;
using GFT_ClubHouse__Management.Libs.Language;
using GFT_ClubHouse__Management.Libs.Login;
using GFT_ClubHouse__Management.Libs.Security;
using Microsoft.AspNetCore.Mvc;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.Enum;
using GFT_ClubHouse__Management.Repositories.Interfaces;

namespace GFT_ClubHouse__Management.Controllers {
    [Route("{Action=index}")]
    public class HomeController : Controller {

        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private LoginUser _loginUser;
        private static readonly MD5HashTools MD5HashTools = new MD5HashTools();


        public HomeController(IEventRepository eventRepository, LoginUser loginUser, IUserRepository userRepository) {
            _eventRepository = eventRepository;
            _loginUser = loginUser;
            _userRepository = userRepository;
        }

        public IActionResult Index() {
            var events = _eventRepository.GetAll();
            return View(events);
        }

        public IActionResult Login() {
            return View();
        }
        
        [HttpPost]
        public IActionResult Login([FromForm] User user) {
            var authenticatedUser = _userRepository.Login(user.Email, MD5HashTools.ReturnMD5(user.Password), UserRoles.User);

            if (authenticatedUser != null) {
                _loginUser.Login(authenticatedUser);
                TempData["MSG_S"] = SuccessMessages.MSG_S008;
                return RedirectToAction("Orders");
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
        public IActionResult Register([FromForm] User user) {
            if (ModelState.IsValid) {
                try {
                    user.Password = MD5HashTools.ReturnMD5(user.Password);
                    user.Roles = UserRoles.User;
                    _userRepository.Insert(user);
                    TempData["MSG_S"] = SuccessMessages.MSG_S001;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception) {
                    TempData["MSG_E"] = ErrorMessages.MSG_E007;
                }
            }
            return View();
        }
        
        [UserAuthorization]
        public IActionResult Orders() {
            var user = _loginUser.GetUser();

            if (user != null) {
                //TODO - Search for all tickets purchased by the user.    
            }
            return View();
        }
    }
}
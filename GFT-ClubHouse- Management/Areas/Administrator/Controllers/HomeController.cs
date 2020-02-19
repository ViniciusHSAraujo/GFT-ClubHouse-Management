using GFT_ClubHouse__Management.Libs.Filters.Security;
using GFT_ClubHouse__Management.Libs.Language;
using GFT_ClubHouse__Management.Libs.Login;
using GFT_ClubHouse__Management.Libs.Security;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.Enum;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Areas.Administrator.Controllers {
    [Area("Administrator")]
    [Route("{Area}/{Action=index}")]
    public class HomeController : Controller {

        private readonly IUserRepository _userRepository;
        private readonly LoginAdmin _loginAdmin;
        private readonly IClubHouseRepository _clubHouseRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;
        private static readonly MD5HashTools MD5HashTools = new MD5HashTools();

        
        public HomeController(IUserRepository userRepository, LoginAdmin loginAdmin, IClubHouseRepository clubHouseRepository, IEventRepository eventRepository, ITicketRepository ticketRepository) {
            _userRepository = userRepository;
            _loginAdmin = loginAdmin;
            _clubHouseRepository = clubHouseRepository;
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
        }

        [AdminAuthorization]
        public IActionResult Index() {
            ViewBag.CountClubHouses = _clubHouseRepository.Count();
            ViewBag.CountEvents = _eventRepository.CountThisMonthEvents();
            ViewBag.CountUsers = _userRepository.Count();
            ViewBag.CountTickets = _ticketRepository.CountTicketsSoldThisMonth();
            return View();
        }

        public IActionResult Login() {
            if (_loginAdmin.GetUser() != null) {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        
        public IActionResult Logout() {
            _loginAdmin.Logout();
            TempData["MSG_S"] = SuccessMessages.MSG_S007;
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public IActionResult Login([FromForm] User user) {
            var authenticatedUser = _userRepository.Login(user.Email, MD5HashTools.ReturnMD5(user.Password), UserRoles.Admin);

            if (authenticatedUser != null) {
                _loginAdmin.Login(authenticatedUser);
                TempData["MSG_S"] = SuccessMessages.MSG_S008;
                return RedirectToAction("Index", new { area = "Administrator" });
            }
            TempData["MSG_E"] = ErrorMessages.MSG_E008;
            return View();
        }
        
    }
}
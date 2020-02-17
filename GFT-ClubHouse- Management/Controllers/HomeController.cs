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
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;

namespace GFT_ClubHouse__Management.Controllers {
    [Route("{Action=index}")]
    public class HomeController : Controller {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly ITicketRepository _ticketRepository;
        private LoginUser _loginUser;
        private static readonly MD5HashTools MD5HashTools = new MD5HashTools();


        public HomeController(IEventRepository eventRepository, LoginUser loginUser, IUserRepository userRepository,
            ISaleRepository saleRepository, ITicketRepository ticketRepository) {
            _eventRepository = eventRepository;
            _loginUser = loginUser;
            _userRepository = userRepository;
            _saleRepository = saleRepository;
            _ticketRepository = ticketRepository;
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
            var authenticatedUser =
                _userRepository.Login(user.Email, MD5HashTools.ReturnMD5(user.Password), UserRoles.User);

            if (authenticatedUser != null) {
                _loginUser.Login(authenticatedUser);
                TempData["MSG_S"] = SuccessMessages.MSG_S008;
                return RedirectToAction("Index");
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
        [HttpPost]
        public IActionResult Checkout([FromForm] Sale sale) {
            sale.Event = _eventRepository.GetById(sale.EventId);
            var ticketsLeft = sale.Event.TicketsLeft();
            if (sale.Quantity > ticketsLeft) {
                TempData["MSG_E"] = $"Oops.. There are only {ticketsLeft} tickets left.";
                return RedirectToAction("Details", "Events", new {id = sale.EventId});
            }

            sale.SinglePrice = sale.Event.Price;
            //sale.UserId = _loginUser.GetUser().Id;
            return View(sale);
        }


        [UserAuthorization]
        [HttpPost]
        public IActionResult Finish([FromForm] Sale sale) {
            sale.Event = _eventRepository.GetById(sale.EventId);
            var ticketsLeft = sale.Event.TicketsLeft();
            if (sale.Quantity > ticketsLeft) {
                TempData["MSG_E"] = $"Oops.. There are only {ticketsLeft} tickets left.";
                return RedirectToAction("Details", "Events", new {id = sale.EventId});
            }

            sale = new Sale() {
                Id = 0,
                Quantity = sale.Quantity,
                EventId = sale.EventId,
                UserId = _loginUser.GetUser().Id,
                SinglePrice = sale.Event.Price
            };

            _saleRepository.Insert(sale);
            _ticketRepository.MarkAsSold(sale.Quantity, sale.EventId, sale.UserId, sale.Id);
            TempData["MSG_S"] = $"Your purchase was successfully done! Order nº {sale.Id:000000}";
            return RedirectToAction("Index", "Orders");
        }
    }
}
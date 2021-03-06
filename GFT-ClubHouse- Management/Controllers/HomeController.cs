﻿using System;
using System.Collections.Generic;
using GFT_ClubHouse__Management.Libs.Filters.Security;
using GFT_ClubHouse__Management.Libs.Login;
using GFT_ClubHouse__Management.Libs.Security;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.ViewModels;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("{Action=index}")]
    public class HomeController : Controller {
        private static readonly MD5HashTools MD5HashTools = new MD5HashTools();
        private readonly IEventRepository _eventRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly ITicketRepository _ticketRepository;
        private LoginUser _loginUser;


        public HomeController(IEventRepository eventRepository, LoginUser loginUser, IUserRepository userRepository,
            ISaleRepository saleRepository, ITicketRepository ticketRepository) {
            _eventRepository = eventRepository;
            _loginUser = loginUser;
            _saleRepository = saleRepository;
            _ticketRepository = ticketRepository;
        }

        public IActionResult Index(int? page, string search) {
            var events = new NextAndRecentsEventsViewModels() {
                UpcomingEvents = _eventRepository.GetNext(9),
                RecentlyAddedEvents = _eventRepository.GetAll(page, search)
            };

            return View(events);
        }


        [UserAuthorization]
        [HttpPost]
        public IActionResult Checkout([FromForm] Sale sale) {
            sale.Event = _eventRepository.GetById(sale.EventId);
            var ticketsLeft = sale.Event.Capacity - _ticketRepository.CountTicketsSoldForAnEvent(sale.EventId);
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
            var ticketsLeft = sale.Event.Capacity - _ticketRepository.CountTicketsSoldForAnEvent(sale.EventId);
            if (sale.Quantity > ticketsLeft) {
                TempData["MSG_E"] = $"Oops.. There are only {ticketsLeft} tickets left.";
                return RedirectToAction("Details", "Events", new {id = sale.EventId});
            }

            sale = new Sale() {
                Id = 0,
                Quantity = sale.Quantity,
                EventId = sale.EventId,
                UserId = _loginUser.GetUser().Id,
                SinglePrice = sale.Event.Price,
                Date = DateTime.Now
            };

            _saleRepository.Insert(sale);

            var tickets = new List<Ticket>();

            for (var i = 0; i < sale.Quantity; i++)
                tickets.Add(new Ticket() {
                    Id = 0,
                    Hash = Guid.NewGuid(),
                    EventId = sale.EventId,
                    SaleId = sale.Id,
                    UserId = sale.UserId
                });

            _ticketRepository.Insert(tickets);

            _saleRepository.Save();

            TempData["MSG_S"] = $"Your purchase was successfully done! Order nº {sale.Id:000000}";
            return RedirectToAction("Index", "Orders", new {Area = "Users"});
        }
    }
}
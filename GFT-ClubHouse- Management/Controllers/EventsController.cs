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
using GFT_ClubHouse__Management.Models.ViewModels;
using GFT_ClubHouse__Management.Repositories;
using GFT_ClubHouse__Management.Repositories.Interfaces;

namespace GFT_ClubHouse__Management.Controllers {
    public class EventsController : Controller {

        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;

        public EventsController(IEventRepository eventRepository, ITicketRepository ticketRepository) {
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
        }

        public IActionResult Details(int id) {
            var eventAndSaleViewModel = new EventSaleViewModel() {
                Event = _eventRepository.GetById(id),
                Sale = new Sale(),
                TicketsLeft = _ticketRepository.CountRemainingTicketsForAnEvent(id)
            };
            return View(eventAndSaleViewModel);
        }
    }
}
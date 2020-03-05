using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.ViewModels;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GFT_ClubHouse__Management.Controllers {
    [ApiExplorerSettings(IgnoreApi = true)]
    public class EventsController : Controller {
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;

        public EventsController(IEventRepository eventRepository, ITicketRepository ticketRepository) {
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
        }

        public IActionResult Details(int id) {
            var event_ = _eventRepository.GetById(id);
            var eventAndSaleViewModel = new EventSaleViewModel() {
                Event = event_,
                Sale = new Sale(),
                TicketsLeft = event_.Capacity - _ticketRepository.CountTicketsSoldForAnEvent(id)
            };
            return View(eventAndSaleViewModel);
        }
    }
}
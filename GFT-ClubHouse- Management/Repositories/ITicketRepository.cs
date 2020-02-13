using System;
using System.Linq;
using GFT_ClubHouse__Management.Data;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;

namespace GFT_ClubHouse__Management.Repositories {
    public class TicketRepository : ITicketRepository {
        private readonly ApplicationDbContext _dbContext;

        public TicketRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public void MarkAsSold(int quantity, int eventId, int userId) {
            var tickets = _dbContext.Set<Ticket>().Where(x => !x.IsSold && x.EventId == eventId).Take(quantity)
                .ToList();

            foreach (var ticket in tickets) {
                ticket.Hash = Guid.NewGuid();
                ticket.IsSold = true;
                ticket.UserId = userId;
            }

            _dbContext.SaveChanges();
        }
    }
}
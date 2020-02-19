using System;
using System.Collections.Generic;
using System.Linq;
using GFT_ClubHouse__Management.Data;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GFT_ClubHouse__Management.Repositories {
    public class TicketRepository : ITicketRepository {
        private readonly ApplicationDbContext _dbContext;

        public TicketRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public void Insert(IEnumerable<Ticket> objs) {
            _dbContext.Set<Ticket>().AddRange(objs);

        }

        public int CountTicketsSoldForAnEvent(int eventId) {
            return _dbContext.Set<Ticket>().Count(x => x.EventId == eventId);
        }

        public int CountTicketsSoldThisMonth() {
            return _dbContext.Set<Ticket>().Include(x => x.Sale).Count(x =>
                x.Sale.Date.Month == DateTime.Now.Month && x.Sale.Date.Year == DateTime.Now.Year);
        }
    }
}
using GFT_ClubHouse__Management.Data;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFT_ClubHouse__Management.Repositories {
    public class EventRepository : IEventRepository {
        private readonly ApplicationDbContext _dbContext;

        public EventRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public int Count() {
            return _dbContext.Set<Event>().Count();
        }

        public IEnumerable<Event> GetAll() {
            return _dbContext.Set<Event>().Include(x => x.ClubHouse).Include(x => x.MusicalGenre)
                .Include(x => x.Tickets).AsNoTracking()
                .ToList();
        }

        public Event GetById(object id) {
            return _dbContext.Set<Event>().Include(x => x.ClubHouse).Include(x => x.MusicalGenre)
                .Include(x => x.Tickets).AsNoTracking()
                .FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Insert(Event obj) {
            obj.Tickets = new List<Ticket>();
            for (int i = 0; i < obj.Capacity; i++) {
                obj.Tickets.Add(new Ticket() {
                    Id = 0,
                    Hash = null,
                    Event = obj,
                    IsSold = false
                });
            }

            _dbContext.Set<Event>().Add(obj);
            Save();
        }

        public void Update(Event obj) {
            var objDb = GetById(obj.Id);

            if (objDb.Tickets.Count(x => x.IsSold) >= obj.Capacity) {
                throw new Exception("You are trying to set a capacity below the number of tickets sold.");
            }

            _dbContext.Set<Event>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            var dif = obj.Capacity - objDb.Capacity;

            if (dif < 0) {
                var ticketsToRemove = _dbContext.Set<Ticket>().Where(x => x.EventId == obj.Id && !x.IsSold)
                    .OrderByDescending(x => x.Id)
                    .Take(Math.Abs(dif)).ToList();
                _dbContext.Set<Ticket>().RemoveRange(ticketsToRemove);
            }
            else {
                for (int i = 0; i < dif; i++) {
                    _dbContext.Set<Ticket>().Add(new Ticket() {
                        Id = 0,
                        Hash = null,
                        Event = obj,
                        IsSold = false
                    });
                }
            }
            Save();
        }

        public void Delete(object id) {
            Event existing = _dbContext.Set<Event>().Find(id);
            _dbContext.Set<Event>().Remove(existing);
            Save();
        }

        public void Save() {
            _dbContext.SaveChanges();
        }
    }
}
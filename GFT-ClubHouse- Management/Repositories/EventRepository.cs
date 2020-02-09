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
        private ApplicationDbContext _dbContext;

        public EventRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IEnumerable<Event> GetAll() {
            return _dbContext.Set<Event>().Include(x => x.ClubHouse).Include(x => x.MusicalGenre).AsNoTracking().ToList();
        }

        public Event GetById(object id) {
            return _dbContext.Set<Event>().Include(x => x.ClubHouse).Include(x => x.MusicalGenre).AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Insert(Event obj) {
            _dbContext.Set<Event>().Add(obj);
            Save();
        }

        public void Update(Event obj) {
            _dbContext.Set<Event>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
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

using System;
using System.Collections.Generic;
using System.Linq;
using GFT_ClubHouse__Management.Data;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace GFT_ClubHouse__Management.Repositories {
    public class EventRepository : IEventRepository {
        private readonly ApplicationDbContext _dbContext;

        public EventRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public int Count() {
            return _dbContext.Set<Event>().Count();
        }

        public int CountThisMonthEvents() {
            return _dbContext.Set<Event>()
                .Count(x => x.Date.Month == DateTime.Now.Month && x.Date.Year == DateTime.Now.Year);
        }

        public bool Exists(int id) {
            return _dbContext.Set<Event>().Any(x => x.Id.Equals(id));
        }

        public IEnumerable<Event> GetAll() {
            return _dbContext.Set<Event>().Include(x => x.ClubHouse.Address).Include(x => x.MusicalGenre)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Event> GetNext(int quantity) {
            return _dbContext.Set<Event>().Include(x => x.ClubHouse.Address).Include(x => x.MusicalGenre)
                .AsNoTracking()
                .OrderBy(x => x.Date)
                .Take(quantity)
                .ToList();
        }

        public IPagedList<Event> GetAll(int? page, string search) {
            var pageNumber = page ?? 1;
            const int resultsPerPage = 9;

            if (string.IsNullOrEmpty(search))
                return _dbContext.Set<Event>().Include(x => x.ClubHouse.Address).Include(x => x.MusicalGenre)
                    .ToPagedList(pageNumber, resultsPerPage);

            search = search.Trim().ToLower();
            return _dbContext.Set<Event>().Include(x => x.ClubHouse.Address).Include(x => x.MusicalGenre)
                .Where(t => t.Name.ToLower().Contains(search) || t.ClubHouse.Name.ToLower().Contains(search) ||
                            t.MusicalGenre.Name.ToLower().Contains(search))
                .ToPagedList(pageNumber, resultsPerPage);
        }

        public Event GetById(object id) {
            return _dbContext.Set<Event>().Include(x => x.ClubHouse.Address).Include(x => x.MusicalGenre)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Insert(Event obj) {
            _dbContext.Set<Event>().Add(obj);
            Save();
        }

        public void Update(Event obj) {
            var ticketsSold = _dbContext.Set<Ticket>().Count(x => x.EventId == obj.Id);

            if (ticketsSold > obj.Capacity)
                throw new Exception("You are trying to set a capacity below the number of tickets sold.");

            _dbContext.Set<Event>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id) {
            var existing = _dbContext.Set<Event>().Find(id);
            _dbContext.Set<Event>().Remove(existing);
            Save();
        }

        public void Save() {
            _dbContext.SaveChanges();
        }
    }
}
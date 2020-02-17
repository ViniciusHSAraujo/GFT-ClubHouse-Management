﻿using GFT_ClubHouse__Management.Data;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public IEnumerable<Event> GetAll() {
            return _dbContext.Set<Event>().Include(x => x.ClubHouse).Include(x => x.MusicalGenre)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Event> GetNext(int quantity) {
            return _dbContext.Set<Event>().Include(x => x.ClubHouse).Include(x => x.MusicalGenre)
                .AsNoTracking()
                .OrderBy(x => x.Date)
                .Take(quantity)
                .ToList();
        }

        public IPagedList<Event> GetAll(int? page, string search) {
            int pageNumber = page ?? 1;
            const int resultsPerPage = 9;

            if (string.IsNullOrEmpty(search)) {
                return _dbContext.Set<Event>().Include(x => x.ClubHouse).Include(x => x.MusicalGenre)
                    .ToPagedList(pageNumber, resultsPerPage);
            }

            search = search.Trim().ToLower();
            return _dbContext.Set<Event>().Include(x => x.ClubHouse).Include(x => x.MusicalGenre)
                .Include(x => x.Tickets)
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
            var ticketsLeft = _dbContext.Set<Ticket>().Where(x => x.EventId == obj.Id).Count(x => !x.IsSold);
            if (ticketsLeft >= obj.Capacity) {
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
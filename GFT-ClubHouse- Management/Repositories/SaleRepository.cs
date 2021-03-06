﻿using System;
using System.Collections.Generic;
using System.Linq;
using GFT_ClubHouse__Management.Data;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace GFT_ClubHouse__Management.Repositories {
    public class SaleRepository : ISaleRepository {
        private readonly ApplicationDbContext _dbContext;

        public SaleRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public int Count() {
            return _dbContext.Set<Sale>().Count();
        }

        public bool Exists(int id) {
            return _dbContext.Set<Sale>().Any(x => x.Id.Equals(id));
        }

        public IEnumerable<Sale> GetAll() {
            return _dbContext.Set<Sale>().AsNoTracking().ToList();
        }

        public IPagedList<Sale> GetAll(int? page, string search) {
            var pageNumber = page ?? 1;
            const int resultsPerPage = 10;

            if (string.IsNullOrEmpty(search))
                return _dbContext.Set<Sale>()
                    .Include(x => x.Event.ClubHouse)
                    .Include(x => x.Event.MusicalGenre)
                    .ToPagedList(pageNumber, resultsPerPage);

            search = search.Trim().ToLower();
            return _dbContext.Set<Sale>()
                .Include(x => x.Event.ClubHouse)
                .Include(x => x.Event.MusicalGenre)
                .Where(t => t.Event.Name.ToLower().Contains(search) ||
                            t.Event.ClubHouse.Name.ToLower().Contains(search) ||
                            t.Event.MusicalGenre.Name.ToLower().Contains(search) ||
                            t.Event.Date.ToLocalTime().ToString().Contains(search))
                .OrderByDescending(x => x.Id)
                .ToPagedList(pageNumber, resultsPerPage);
        }

        public Sale GetById(object id) {
            return _dbContext.Set<Sale>().Include(x => x.Event.ClubHouse.Address).Include(x => x.Event.MusicalGenre)
                .Include(x => x.User).Include(x => x.Tickets).AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
        }

        public IEnumerable<Sale> GetByUser(int userId) {
            return _dbContext.Set<Sale>().Include(x => x.Event.ClubHouse.Address).Include(x => x.Event.MusicalGenre)
                .Include(x => x.User).Include(x => x.Tickets).AsNoTracking().Where(x => x.UserId.Equals(userId))
                .AsEnumerable();
        }

        public IPagedList<Sale> GetByUser(int userId, int? page, string search) {
            var pageNumber = page ?? 1;
            const int resultsPerPage = 10;

            if (string.IsNullOrEmpty(search))
                return _dbContext.Set<Sale>()
                    .Include(x => x.Event.ClubHouse)
                    .Include(x => x.Event.MusicalGenre)
                    .Where(x => x.UserId == userId)
                    .ToPagedList(pageNumber, resultsPerPage);

            search = search.Trim().ToLower();
            return _dbContext.Set<Sale>()
                .Include(x => x.Event.ClubHouse)
                .Include(x => x.Event.MusicalGenre)
                .Where(t => t.UserId == userId &&
                            (t.Event.Name.ToLower().Contains(search) ||
                             t.Event.ClubHouse.Name.ToLower().Contains(search) ||
                             t.Event.MusicalGenre.Name.ToLower().Contains(search) ||
                             t.Event.Date.ToLocalTime().ToString().Contains(search)))
                .OrderByDescending(x => x.Id)
                .ToPagedList(pageNumber, resultsPerPage);
        }

        public void Insert(Sale obj) {
            _dbContext.Set<Sale>().Add(obj);
        }

        public void Update(Sale obj) {
            _dbContext.Set<Sale>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id) {
            var existing = _dbContext.Set<Sale>().Find(id);
            _dbContext.Set<Sale>().Remove(existing);
            Save();
        }

        public void Save() {
            _dbContext.SaveChanges();
        }

        public List<SelectListItem> GetSelectList() {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using GFT_ClubHouse__Management.Data;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace GFT_ClubHouse__Management.Repositories {
    public class ClubHouseRepository : IClubHouseRepository {
        private readonly ApplicationDbContext _dbContext;

        public ClubHouseRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public int Count() {
            return _dbContext.Set<ClubHouse>().Count();
        }

        public bool Exists(int id) {
            return _dbContext.Set<ClubHouse>().Any(x => x.Id.Equals(id));
        }

        public IEnumerable<ClubHouse> GetAll() {
            return _dbContext.Set<ClubHouse>().Include(x => x.Address).AsNoTracking().ToList();
        }

        public IPagedList<ClubHouse> GetAll(int? page, string search) {
            var pageNumber = page ?? 1;
            const int resultsPerPage = 10;

            if (string.IsNullOrEmpty(search))
                return _dbContext.Set<ClubHouse>().ToPagedList(pageNumber, resultsPerPage);

            search = search.Trim().ToLower();
            return _dbContext.Set<ClubHouse>()
                .Where(t => t.Name.ToLower().Contains(search))
                .ToPagedList(pageNumber, resultsPerPage);
        }

        public ClubHouse GetById(object id) {
            return _dbContext.Set<ClubHouse>().Include(x => x.Address).AsNoTracking()
                .FirstOrDefault(x => x.Id.Equals(id));
        }

        public List<ClubHouse> GetAllByName(string name) {
            return _dbContext.Set<ClubHouse>().Include(x => x.Address).AsNoTracking()
                .Where(x => x.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public List<SelectListItem> GetSelectList() {
            return _dbContext.Set<ClubHouse>()
                .Select(x => new SelectListItem() {Value = x.Id.ToString(), Text = x.Name}).AsNoTracking().ToList();
        }


        public void Insert(ClubHouse obj) {
            _dbContext.Set<ClubHouse>().Add(obj);
            Save();
        }

        public void Update(ClubHouse obj) {
            _dbContext.Set<ClubHouse>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id) {
            var existing = _dbContext.Set<ClubHouse>().Find(id);
            _dbContext.Set<ClubHouse>().Remove(existing);
            Save();
        }

        public void Save() {
            _dbContext.SaveChanges();
        }
    }
}
using GFT_ClubHouse__Management.Data;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFT_ClubHouse__Management.Repositories {
    public class ClubHouseRepository : IClubHouseRepository {
        private readonly ApplicationDbContext _dbContext;

        public ClubHouseRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public int Count() {
            return _dbContext.Set<ClubHouse>().Count();
        }
        
        public IEnumerable<ClubHouse> GetAll() {
            return _dbContext.Set<ClubHouse>().Include(x => x.Address).AsNoTracking().ToList();
        }

        public ClubHouse GetById(object id) {
            return _dbContext.Set<ClubHouse>().Include(x => x.Address).AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
        }

        public List<SelectListItem> GetSelectList() {
            return _dbContext.Set<ClubHouse>().Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).AsNoTracking().ToList();
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
            ClubHouse existing = _dbContext.Set<ClubHouse>().Find(id);
            _dbContext.Set<ClubHouse>().Remove(existing);
            Save();
        }

        public void Save() {
            _dbContext.SaveChanges();
        }
    }
}

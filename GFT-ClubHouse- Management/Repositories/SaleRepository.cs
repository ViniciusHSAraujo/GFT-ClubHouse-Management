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
    public class SaleRepository : ISaleRepository {
        private readonly ApplicationDbContext _dbContext;

        public SaleRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }
        
        public int Count() {
            return _dbContext.Set<Sale>().Count();
        }

        public IEnumerable<Sale> GetAll() {
            return _dbContext.Set<Sale>().AsNoTracking().ToList();
        }

        public Sale GetById(object id) {
            return _dbContext.Set<Sale>().Include(x => x.Event.ClubHouse.Address).Include(x => x.Event.MusicalGenre).Include(x => x.User).Include(x => x.Tickets).AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
        }
        
        public IEnumerable<Sale> GetByUser(int id) {
            return _dbContext.Set<Sale>().Include(x => x.Event.ClubHouse.Address).Include(x => x.Event.MusicalGenre).Include(x => x.User).Include(x => x.Tickets).AsNoTracking().Where(x => x.UserId.Equals(id)).AsEnumerable();

        }

        public List<SelectListItem> GetSelectList() {
            throw new NotImplementedException();
        }

        public void Insert(Sale obj) {
            _dbContext.Set<Sale>().Add(obj);
            Save();
        }

        public void Update(Sale obj) {
            _dbContext.Set<Sale>().Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id) {
            Sale existing = _dbContext.Set<Sale>().Find(id);
            _dbContext.Set<Sale>().Remove(existing);
            Save();
        }

        public void Save() {
            _dbContext.SaveChanges();
        }
    }
}

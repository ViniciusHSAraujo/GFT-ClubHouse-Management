using GFT_ClubHouse__Management.Data;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFT_ClubHouse__Management.Repositories {
    public class GenericRepository<T> : IGenericRepository<T> where T : class {

        private ApplicationDbContext _dbContext;
        private DbSet<T> table;

        public GenericRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
            table = _dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll() {
            return table.AsNoTracking().ToList();
        }

        public T GetById(object id) {
            return table.Find(id);
        }

        public void Insert(T obj) {
            table.Add(obj);
            Save();
        }

        public void Update(T obj) {
            table.Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(object id) {
            T existing = table.Find(id);
            table.Remove(existing);
            Save();
        }

        public void Save() {
            _dbContext.SaveChanges();
        }

    }
}
    
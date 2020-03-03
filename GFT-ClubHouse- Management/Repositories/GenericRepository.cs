using GFT_ClubHouse__Management.Data;
using GFT_ClubHouse__Management.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GFT_ClubHouse__Management.Repositories {
    public class GenericRepository<T> : IGenericRepository<T> where T : class {

        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> table;

        public GenericRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
            table = _dbContext.Set<T>();
        }

        public int Count() {
            return _dbContext.Set<T>().Count();
        }

        public bool Exists() {
            throw new NotImplementedException();
        }

        public bool Exists(int id) {
            throw new NotImplementedException();
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

        public IPagedList<T> GetAll(int? page, string search) {
            throw new NotImplementedException();
        }
    }
}
    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GFT_ClubHouse__Management.Repositories.Interfaces {
    public interface IGenericRepository<T> where T : class {
        bool Exists(int id);
        IEnumerable<T> GetAll();
        IPagedList<T> GetAll(int? page, string search);
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        int Count();
        void Save();

    }
}
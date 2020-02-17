using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GFT_ClubHouse__Management.Repositories.Interfaces {
    public interface IGenericRepository<T> where T : class {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        int Count();
        void Save();
        IPagedList<T> List(int? page, string search);

    }
}
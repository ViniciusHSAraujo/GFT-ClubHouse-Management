using System.Collections.Generic;
using GFT_ClubHouse__Management.Models;
using X.PagedList;

namespace GFT_ClubHouse__Management.Repositories.Interfaces {
    public interface ISaleRepository : IGenericRepository<Sale> {
        IEnumerable<Sale> GetByUser(int userId);
        IPagedList<Sale> GetByUser(int userId, int? page, string search);
    }
}
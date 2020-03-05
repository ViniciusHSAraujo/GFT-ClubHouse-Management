using System.Collections.Generic;
using GFT_ClubHouse__Management.Models;

namespace GFT_ClubHouse__Management.Repositories.Interfaces {
    public interface IEventRepository : IGenericRepository<Event> {
        IEnumerable<Event> GetNext(int quantity);
        int CountThisMonthEvents();
    }
}
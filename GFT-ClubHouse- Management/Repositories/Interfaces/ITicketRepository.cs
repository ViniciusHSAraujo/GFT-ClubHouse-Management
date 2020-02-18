using System.Collections;
using System.Collections.Generic;
using GFT_ClubHouse__Management.Models;

namespace GFT_ClubHouse__Management.Repositories.Interfaces {
    public interface ITicketRepository {
        
        void Insert(IEnumerable<Ticket> objs);
        int CountTicketsSoldForAnEvent(int eventId);
    }
}
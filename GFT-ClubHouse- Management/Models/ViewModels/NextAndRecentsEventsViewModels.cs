using System.Collections;
using System.Collections.Generic;
using X.PagedList;

namespace GFT_ClubHouse__Management.Models.ViewModels {
    public class NextAndRecentsEventsViewModels {

        public IEnumerable<Event> UpcomingEvents { get; set; }
        
        public IPagedList<Event> RecentlyAddedEvents { get; set; } 
    }
}
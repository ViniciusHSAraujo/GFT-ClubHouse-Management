using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFT_ClubHouse__Management.Models {
    public class Ticket {
        public int Id { get; set; }

        public Guid? Hash { get; set; }
        
        public int EventId { get; set; }
        
        [ForeignKey("EventId")] 
        public virtual Event Event { get; set; }

        public bool IsSold { get; set; }
    }
}
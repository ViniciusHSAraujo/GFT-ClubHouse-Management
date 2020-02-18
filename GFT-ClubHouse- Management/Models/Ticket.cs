using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFT_ClubHouse__Management.Models {
    public class Ticket {
        public int Id { get; set; }

        public Guid? Hash { get; set; }

        public int EventId { get; set; }

        [ForeignKey("EventId")] public virtual Event Event { get; set; }
        
        public int? UserId { get; set; }

        [ForeignKey("UserId")] public virtual User User { get; set; }
        
        public int? SaleId { get; set; }

        [ForeignKey("SaleId")] public virtual Sale Sale { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GFT_ClubHouse__Management.Libs.Language;

namespace GFT_ClubHouse__Management.Models {
    public class Sale {
        public int Id { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        public int Quantity { get; set; }

        [Display(Name = "Single Price")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        public double SinglePrice { get; set; }

        [Display(Name = "Total Price")] public double TotalPrice => SinglePrice * Quantity;

        [Display(Name = "Date")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        public DateTime Date { get; set; }

        [Display(Name = "Event")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        public int EventId { get; set; }

        [ForeignKey("EventId")] public virtual Event Event { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")] public virtual User User { get; set; }

        public virtual List<Ticket> Tickets { get; set; }
    }
}
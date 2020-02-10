using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GFT_ClubHouse__Management.Libs.Language;

namespace GFT_ClubHouse__Management.Models {
    public class Event {
        public int Id { get; set; }

        [Display(Name = "Event Name")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(80, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E003")]
        public string Name { get; set; }

        [Display(Name = "Capacity")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [Range(1, 80000, ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = "MSG_E011")]
        public int Capacity { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        public DateTime Date { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [Range(1, 10000, ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = "MSG_E011")]
        public double Price { get; set; }

        [Display(Name = "Club House")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        public int ClubHouseId { get; set; }

        [ForeignKey("ClubHouseId")] public virtual ClubHouse ClubHouse { get; set; }

        [Display(Name = "Musical Genre")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        public int MusicalGenreId { get; set; }

        [ForeignKey("MusicalGenreId")] public virtual MusicalGenre MusicalGenre { get; set; }

        public virtual List<Ticket> Tickets { get; set; }
    }
}
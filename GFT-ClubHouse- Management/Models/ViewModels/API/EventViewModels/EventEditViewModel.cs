using System;
using System.ComponentModel.DataAnnotations;
using GFT_ClubHouse__Management.Libs.Language;

namespace GFT_ClubHouse__Management.Models.ViewModels.API.EventViewModels {
    public class EventEditViewModel {
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(80, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E003")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [Range(1, 80000, ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = "MSG_E011")]
        public int Capacity { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        [Range(1, 10000, ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = "MSG_E011")]
        public double Price { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        public int ClubHouseId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "MSG_E001")]
        public int MusicalGenreId { get; set; }
    }
}